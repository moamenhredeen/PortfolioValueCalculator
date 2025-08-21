using System.Globalization;
using System.Text;
using Application.UseCases.Investor.CalculatePortfolioValue;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace Cli;

public class App(CalculatePortfolioValueUseCase  calculatePortfolioValue, ILogger<App> logger)
{
    private const string CALCULATE_INVESTOR_PORTFOLIO_VALUE = "Calculate Investor Portflio Value";
    private const string EXIT = "Exit";
    
    
    public void Run()
    {
        var running = true;
        _configureConsole();
        var rootMenu = new SelectionPrompt<string>()
            .AddChoices(CALCULATE_INVESTOR_PORTFOLIO_VALUE, EXIT)
            .WrapAround();
        
        // start
        while (running)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule($"[blue] QPLIX CLI [/]"));
            var choice = AnsiConsole.Prompt(rootMenu);
            switch (choice)
            {
                case CALCULATE_INVESTOR_PORTFOLIO_VALUE:
                    logger.LogDebug("Calculate Investor Portfolio Value");
                    var id = AnsiConsole.Prompt(new TextPrompt<string>("Enter investor Id: "));
                    var date = AnsiConsole.Prompt(new TextPrompt<DateTime>("Enter date: "));
                    logger.LogInformation($"Calculate Portfolio Value for Investor {id} on {date:yyyy-MM-dd}");
                    var result = calculatePortfolioValue.Execute(new  CalculatePortfolioValueRequest(id, date));
                    if (result.IsSuccess) AnsiConsole.WriteLine(result.Value.portfolioValue);
                    else AnsiConsole.WriteLine(result.ErrorMessage);
                    var more = AnsiConsole.Prompt(new TextPrompt<bool>("One More Time ?")
                        .AddChoice(true)
                        .AddChoice(false)
                        .DefaultValue(true)
                        .WithConverter(choice => choice ? "Y" : "n"));
                    if (!more) running = false;
                    break;
                case EXIT:
                    logger.LogDebug("exit application");
                    running = false;
                    break;
            }
        }
    }
    
    private void _configureConsole()
    {
        CultureInfo culture = new CultureInfo("en-US");
        AnsiConsole.Profile.Encoding = Encoding.UTF8;
    }
    
}