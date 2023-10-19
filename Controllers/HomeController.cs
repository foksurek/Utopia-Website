using System.Diagnostics;
using System.Web.WebPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities;
using Utopia.Contexts;
using Utopia.Interfaces;
using Utopia.Models;
using Utopia.Models.ApiModels;
using Utopia.Models.SqlModels;
using Utopia.Packages;
using Utopia.ViewModels;

namespace Utopia.Controllers;

public class HomeController : Controller
{
    private readonly ApiUrls _apiUrls;
    private readonly IRequests _requests;
    private readonly AppDbContext _dbContext;
    
    public HomeController(IOptions<ApiUrls> apiUrls, IRequests requests, AppDbContext dbContext)
    {
        _requests = requests;
        _dbContext = dbContext;
        _apiUrls = apiUrls.Value;
    }
    
    
    public async Task<ActionResult> Index()
    {
        var html = await _requests.GetAsync(_apiUrls.GetLeaderboardTop);
        ViewBag.best = html;
        html = await _requests.GetAsync(_apiUrls.GetLeaderboardTopRelax);
        ViewBag.bestrx = html;
        html = await _requests.GetAsync(_apiUrls.GetPlayerCount);
        var jObject = JObject.Parse(html);
        jObject = JObject.Parse(jObject["counts"]?.ToString()!);
        ViewBag.online = jObject["online"]!;
        
        return View();
    }

    public ActionResult Privacy()
    {
        return View();
    }
    
    

    [Route("beatmaps/{id}")]
    public RedirectResult Beatmaps(int id)
    {
        return Redirect("https://osu.ppy.sh/beatmaps/" + id);
    }

    public ActionResult Maps()
    {
        return View();
    }
    
    [Route("leaderboards")]
    public async Task<ActionResult> Rankings()
    {
        string mode = HttpContext.Request.Query["mode"];
        var imode = 0;
        if (int.TryParse(mode, out _)) imode = int.Parse(mode);
        if (!((imode >= 1 && imode <= 6) || imode == 8)) imode = 0;
        var query = await _dbContext.Stats
            .Join(_dbContext.SqlUsers, s => s.Id, u => u.Id, (s, u) => new { s, u })
            .OrderByDescending(result => result.s.PP)
            .Where(result => result.s.Mode == imode)
            .Where(result => result.s.PlayCount > 0)
            .Select(result => new LeaderboardViewModel
            {
                Name = result.u.Name,
                Pp = result.s.PP,
                Id = result.u.Id,
                PlayCount = result.s.PlayCount,
                Acc = result.s.Acc,
                Country = result.u.Country,
                DonorEnd = result.u.DonorEnd,
                Priv = result.u.Priv,
                TScore = result.s.TScore,
            }).ToListAsync();
        
        @ViewBag.results = query;
        @ViewBag.mode = imode;
        if (query.Count > 0) ViewBag.priv = PrivCalc.GetHighestRank(query[0].Priv)!;


        return View();
    }
    
    [Route("u/{id}")]
    public async Task<ActionResult> UserProfile(string id)
    {
        string mode = HttpContext.Request.Query["mode"];
        var imode = 0;
        if (int.TryParse(mode, out _)) imode = int.Parse(mode);
        if (!((imode >= 1 && imode <= 6) || imode == 8)) imode = 0;
        
        List<SqlUser> users;
        try
        {
            if (int.TryParse(id, out _))
            {
                users = await _dbContext.SqlUsers.Where(u => u.Id == int.Parse(id)).ToListAsync();
            }
            else
            {
                users = await _dbContext.SqlUsers.Where(u => u.Name == id).ToListAsync();
            }
        }
        catch (Exception e)
        {
            @ViewBag.statuscode = 500;
            return View("~/Views/Error/DatabaseError.cshtml");
        }
        
        try
        {
            if  (users[0].Name == "BanchoBot") return View("userNotFound");
            if (users.Count == 0) return View("userNotFound");
            var arg = int.TryParse(id, out _) ? "id=" + id : "name=" + id;
            var info = await _requests.GetAsync(_apiUrls.GetPlayerInfo + $"&mode={imode}&{arg}");
            ViewBag.json = info;
            ViewBag.id = id;
            ViewBag.mode = imode;
            ViewBag.priv = PrivCalc.GetHighestRank(JsonConvert.DeserializeObject<GetPlayerInfo>(info)!.player.info.priv)!;
            
            ViewBag.playerStatus = await _requests.GetAsync(_apiUrls.GetPlayerStatus + "?" + arg);
            
            ViewBag.topplays = await _requests.GetAsync(_apiUrls.GetPlayerTopScores + $"&mode={imode}&limit=25&{arg}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View("userNotFound");
        }

        return View("userProfile");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}