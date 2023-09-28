using Brewery.Web.Helpers;
using Elia.Core.Containers;
using Elia.Core.Utils;
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
        services.AddLocalization();
        services.AddBlazorBootstrap();
        
        return services;
    }
    

    #endregion
}