using System.Web.WebPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Utopia.Interfaces;
using Utopia.Models;
using Utopia.Models.SqlModels;

namespace Utopia.Controllers;

public class SqlController : Controller
{ 
    
    private readonly IRequests _requests;

    public SqlController(IRequests requests)
    {
        _requests = requests;
    }

    public async Task<IActionResult> GetMaps(string term, string offset = "0", int limit = 20, string status = "1")
    {
        try
        {
            string response;
            if (!term.IsEmpty())
            {
                response =
                    await _requests.GetAsync("https://osu.direct/api/search?amount=" + limit + "&offset=" + offset + "&mode=0&status=" + status + "&q=" + term);
            }
            else
            {
                string q = "https://osu.direct/api/search?amount=" + limit + "&offset=0" + offset + "&mode=0&status=" +
                           status + "&q=";
                response = await _requests.GetAsync(q);
            }
            
            var maps = JsonConvert.DeserializeObject<List<Map>>(response);

            var suggestions = maps!.Select(s => new
            {
                ids = s.ChildrenBeatmaps.Select(b => b.BeatmapID),
                s.SetID,
                s.Artist,
                s.Title,
                s.Creator,
                s.RankedStatus
            }).ToList();

            return Json(new { data = suggestions });
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
    
        }
    }
}