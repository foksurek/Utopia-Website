using Microsoft.EntityFrameworkCore;
using Utopia.Contexts;
using Utopia.Interfaces;
using Utopia.Models;
using Utopia.Services;

namespace Utopia;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
            
        builder.Services.AddControllersWithViews();
        builder.Services.AddSession(options =>
        {
            options.Cookie.Name = ".Utopia.Session";
            options.IdleTimeout = TimeSpan.FromSeconds(3600);
            options.Cookie.IsEssential = true;
        });
        
        // ApiUrls

        builder.Services.Configure<ApiUrls>(builder.Configuration.GetSection("ApiUrls"));
        builder.Services.Configure<FilePaths>(builder.Configuration.GetSection("FilePaths"));
        builder.Services.Configure<Urls>(builder.Configuration.GetSection("Urls"));

        builder.Services.AddHttpClient<IRequests, Requests>();
    
        var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
        
        builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString!));
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        
    
        var app = builder.Build();
    
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error/500");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHsts();
        }
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
    
        app.UseRouting();
    
        app.UseAuthorization();
    
        app.MapControllerRoute(
            name: "default",
            pattern: "{action=Index}",
            defaults: new { controller = "Home" });
        app.MapControllerRoute(
            name: "controllers",
            pattern: "{controller}/{action}/");

        app.UseSession();
        
        app.Run();
    }     
}

