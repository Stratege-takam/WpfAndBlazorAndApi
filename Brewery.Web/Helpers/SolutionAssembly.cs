using System.Reflection;

namespace Brewery.Web.Helpers;

/// <summary>
/// 
/// </summary>
public static class SolutionAssembly
{
    /// <summary>
    /// 
    /// </summary>
    public static string Web { get; set; } = "Brewery.Web";
    
    /// <summary>
    /// 
    /// </summary>
    public static string Services { get; set; } = "Brewery.Services";
    
    /// <summary>
    /// 
    /// </summary>
    public static string Core { get; set; } = "Elia.Core";
    

    /// <summary>
    /// 
    /// </summary>
    public static Assembly[] GetAllAssemblies => new string[]
    {
        Core,
        Services,
        Web
    }.Select(s => Assembly.Load(s) ).ToArray();
        
}