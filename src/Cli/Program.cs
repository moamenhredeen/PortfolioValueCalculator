using Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Csv;
using Serilog;
using Spectre.Console;

namespace Cli;

class Program
{
    public static void Main(string[] args)
    {
        // ********************************************************************************************
        // Setup Dependency Injection Container
        // ********************************************************************************************
        
        // initialize service collection
        var services = new ServiceCollection();
        
        // setup config
        var dataFolder = new Dictionary<string, string>()
        {
            ["DataFolder"] = Path.Combine(AppContext.BaseDirectory, "data"),
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(dataFolder!)
            .Build();
        
        // configure logging
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        // register services 
        var serviceProvider = services
            .AddSingleton<IConfiguration>(configuration)
            .AddLogging(loggingBuilder => loggingBuilder.AddSerilog())
            .AddRepositories()
            .AddServices()
            .AddSingleton<App>()
            .BuildServiceProvider();
        
        // ********************************************************************************************
        // Entry Point
        // ********************************************************************************************
        
        try
        {
            serviceProvider.GetRequiredService<ILogger<Program>>().LogDebug("starting the application");
            serviceProvider.GetRequiredService<CsvContext>().Load();
            serviceProvider.GetRequiredService<App>().Run();
        }
        catch (Exception e)
        {
            AnsiConsole.WriteLine("Unhandled exception:");
            AnsiConsole.WriteException(e, ExceptionFormats.NoStackTrace);
        }
    }
}
