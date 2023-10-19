using System.Web.WebPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utopia.Contexts;
using Utopia.Interfaces;
using Utopia.Models;

namespace Utopia.Controllers;

public class ApiController: Controller
{
    private readonly List<User> _users = new();
    private readonly AppDbContext _dbContext;

    public ApiController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<JsonResult> GetSuggestions(string term)
    {
        
        var players = await _dbContext.SqlUsers.ToListAsync();

        foreach (var player in players)
        {
            _users.Add(new User{ Id = player.Id, Name = player.Name});
        }
        
        if (term.IsEmpty())
        {
            var eSuggestions = _users.Take(6).Skip(1).Select(u => new { u.Name, u.Id }).ToList();
            return Json(new { data = eSuggestions });
        }
        
        var suggestions = _users.Where(u => u.Id.ToString() == term || u.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
            .Select(u => new { u.Name, u.Id })
            .ToList();


        return Json(new { data = suggestions });
    }
}