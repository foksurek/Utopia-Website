using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Utopia.Contexts;
using Utopia.Interfaces;
using Utopia.Models;
using Utopia.Services;

namespace Utopia;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Starting ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Utopia...");
        Console.WriteLine();
        string connectionString = ""; 
        //Check required env variables
        dotenv.net.DotEnv.Load();
        var requiredEnvVariables = new List<string>
        {
            "DB_HOST",
            "DB_PORT",
            "DB_NAME",
            "DB_USER",
            "DB_PASS"
        };
        var missing = false;
        foreach (var requiredEnvVariable in requiredEnvVariables)
        {
            if (Environment.GetEnvironmentVariable(requiredEnvVariable) != null) continue;
            ShowError($"Missing required environment variable: {requiredEnvVariable}");
            missing = true;
        }

        if (missing) return;
        
        // Build connection string
        var dbConnectionModel = new DbConnectionModel
        {
            Host = Environment.GetEnvironmentVariable("DB_HOST")!,
            Port = Environment.GetEnvironmentVariable("DB_PORT")!,
            Database = Environment.GetEnvironmentVariable("DB_NAME")!,
            Username = Environment.GetEnvironmentVariable("DB_USER")!,
            Password = Environment.GetEnvironmentVariable("DB_PASS")!
        };
        
        connectionString = $"server={dbConnectionModel.Host};port={dbConnectionModel.Port};database={dbConnectionModel.Database};user={dbConnectionModel.Username};password={dbConnectionModel.Password}";
        
        // Check connection
        
        try
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            connection.Close();
        }
        catch (Exception e)
        {
            ShowError("Cannot connect to database. Check .env file.");
            return;
        }

        Console.ForegroundColor = ConsoleColor.White;
        // App Run        
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
    
    private static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("----------------------------------------");
        Console.WriteLine(message);
        Console.WriteLine("----------------------------------------");
        Console.ForegroundColor = ConsoleColor.White;
    }
}

