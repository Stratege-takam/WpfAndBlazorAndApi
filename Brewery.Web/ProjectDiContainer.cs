using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Brewery.Web.Helpers;
using Brewery.Web.Helpers.States;
using Brewery.Web.States;
using Elia.Core.Containers;
using Elia.Core.Utils;
using Microsoft.AspNetCore.Components.Authorization;

namespace Brewery.Web;


/// <summary>
/// 
/// </summary>
public static class ProjectDiContainer
{
    #region Extensions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddProjectScoped(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(nameof(AppSettings.Server));
        services.Configure<AppSettings.Server>(section);
        services.AutoInject(SolutionAssembly.GetAllAssemblies);
        
        services.AddBlazoredLocalStorage();
        services.AddBlazoredSessionStorage();
        
        services.AddAuthorizationCore();
        services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthSateProvider>());

        services.AddLocalization();
        services.AddBlazorBootstrap();
        
        return services;
    }
    

    #endregion
}