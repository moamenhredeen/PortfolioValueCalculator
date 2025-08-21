using Application.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Csv;

namespace Persistence;

public static class PersistenceServiceCollectionExtensions
{

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<CsvContext>();
        services.AddSingleton<IPortfolioRepository, PortfolioRepository>();
        return services;
    }
}