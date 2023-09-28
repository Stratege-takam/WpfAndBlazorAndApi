using Brewery.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Brewery.DAL;

/// <summary>
/// 
/// </summary>
public static class InfraDiContainer
{
    #region Extensions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfraScoped(this IServiceCollection services, IConfiguration configuration)
    {
        // context
        string connectionString = configuration.GetConnectionString("DefaultConnection");
       
        services.AddDbContextPool<BreweryContext>(options => 
                options.UseSqlServer(connectionString,  o =>
                        o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                        .CommandTimeout((int)TimeSpan.FromMinutes(20).TotalSeconds)
                        .MigrationsAssembly("Brewery.API"))
                        .EnableSensitiveDataLogging() // <-- These two calls are optional but help
                        .EnableDetailedErrors()  // <-- with debugging (remove for production).).EnableSensitiveDataLogging().EnableDetailedErrors()
                );
                       
        return services;
    }

    #endregion
}