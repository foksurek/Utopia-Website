using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Utopia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Utopia.ActionFilters;
using Utopia.Contexts;
using Utopia.Interfaces;
using Utopia.Models.ApiModels;
using Utopia.Packages;
using Utopia.ViewModels;

namespace Utopia.Controllers;
public class AccountController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly ApiUrls _apiUrls;
    private readonly IRequests _requests;

    public AccountController(AppDbContext dbContext, IOptions<ApiUrls> apiUrls, IRequests requests)
    {
        _dbContext = dbContext;
        _requests = requests;
        _apiUrls = apiUrls.Value;
    }
    
    
    [Route("/account/edit")]
    public async Task<IActionResult> Edit()
    {
        if (HttpContext.Session.GetString("UserId") == null)
        {
            return RedirectToAction("Index", "Home");
        }
        
        var id = HttpContext.Session.GetString("UserId")!;
        var info = await _requests.GetAsync(_apiUrls.GetPlayerInfo + $"&id={id}");
        ViewBag.json = info;
        ViewBag.id = id;
        ViewBag.priv = PrivCalc.GetHighestRank(JsonConvert.DeserializeObject<GetPlayerInfo>(info)!.player.info.priv)!;
            
        ViewBag.playerStatus = await _requests.GetAsync(_apiUrls.GetPlayerStatus + $"?id={id}");
            
            
        
        return View();
    }


    public IActionResult ChangePassword()
    {
        return Ok();
    }git


    [HttpPost]
    [CheckLoginStatus]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeName(ChangeNameModel model)
    {
        
        var html = await _requests.GetAsync(_apiUrls.GetPlayerInfo + "&id=" + HttpContext.Session.GetString("UserId"));
            
        ViewBag.json = html;
        
        if (string.IsNullOrEmpty(model.Name))
        {
            ModelState.AddModelError("Name", "This field is required");
            return View("Edit");
        }
        
        if (!ModelState.IsValid) return View("Edit");
        
        var results = 
            await _dbContext.SqlUsers.ToListAsync();
        
        
        if (results.Any(result => result.Name.ToLower() == model.Name.ToLower()))
        {
            ModelState.AddModelError("Name", "This name is already taken.");
            return View("Edit");
        }
        if (!Regex.IsMatch(model.Name, @"^[a-zA-Z0-9\[\]_\(\)\!\@\#\ ]{3,16}$") )
        {
            ModelState.AddModelError("Name", "Wrong username format.");
            return View("Edit");
        }
            
        var user = await _dbContext.SqlUsers.FindAsync(int.Parse(HttpContext.Session.GetString("UserId")!));
        user!.Name = model.Name;
        user!.SafeName = model.Name.Replace(" ", "_").ToLower();
        await _dbContext.SaveChangesAsync();
            
        HttpContext.Session.SetString("UserName", model.Name);
        
        ModelState.Clear();

        return RedirectToAction("Edit");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index", "Home");

        var results = await _dbContext.SqlUsers
            .Where(u => u.Name == model.Username)
            .Select(u => new { u.Id, u.Name, u.Bcrypt })
            .ToListAsync();
        
        var password = model.Password;
        var storedPassword = results.FirstOrDefault()?.Bcrypt;

        if (storedPassword == null)
        {
            return RedirectToAction("Index", "Home");
        }

        if (!CheckPassword(password, storedPassword)) return RedirectToAction("Index", "Home");
        HttpContext.Session.SetString("UserId", results.FirstOrDefault()?.Id.ToString()!);
        HttpContext.Session.SetString("UserName", results.FirstOrDefault()?.Name!);
            
        return RedirectToAction("Index", "Home");
    }

    private static bool CheckPassword(string password, string hashedPassword)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var passwordMd5Bytes = MD5.Create().ComputeHash(passwordBytes);
        var passwordMd5 = BitConverter.ToString(passwordMd5Bytes).Replace("-", "").ToLower();

        return BCrypt.Net.BCrypt.Verify(passwordMd5, hashedPassword);
    }
}