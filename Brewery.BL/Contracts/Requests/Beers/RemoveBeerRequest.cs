using System.ComponentModel.DataAnnotations;

namespace Brewery.BL.Contracts.Requests.Beers;

/// <summary>
/// 
/// </summary>
public class RemoveBeerRequest
{
    /// <summary>
    /// Name or id of beer to removed
    /// </summary>
    [Required]
    public string Name { get; set; }
}