using Application.UseCases.Investor.CalculatePortfolioValue;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<CalculatePortfolioValueUseCase>();
        return services;
    }
}