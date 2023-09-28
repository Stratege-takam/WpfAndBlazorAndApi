using System.ComponentModel.DataAnnotations;

namespace Brewery.Contract.Contracts.Requests.Beers;

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