using System.Reflection;

namespace Brewery.API.Helpers;

/// <summary>
/// 
/// </summary>
public static class SolutionAssembly
{
    /// <summary>
    /// 
    /// </summary>
    public static string Api { get; set; } = "Brewery.API";
    
    /// <summary>
    /// 
    /// </summary>
    public static string Dal { get; set; } = "Brewery.DAL";
    
    /// <summary>
    /// 
    /// </summary>
    public static string Bl { get; set; } = "Brewery.BL";
    
    /// <summary>
    /// 
    /// </summary>
    public static string Core { get; set; } = "Elia.Core";
    
    /// <summary>
    /// 
    /// </summary>
    public static string ASP { get; set; } = "Elia.Share.ASP";

    /// <summary>
    /// 
    /// </summary>
    public static Assembly[] GetAllAssemblies => new string[]
    {
        Core,
        Dal,
        ASP,
        Bl,
        Api
    }.Select(s => Assembly.Load(s) ).ToArray();
        
}