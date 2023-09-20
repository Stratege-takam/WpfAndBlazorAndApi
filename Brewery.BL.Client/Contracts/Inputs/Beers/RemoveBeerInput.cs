using System.ComponentModel.DataAnnotations;

namespace Brewery.BL.Client.Contracts.Inputs.Beers;

/// <summary>
/// 
/// </summary>
public class RemoveBeerInput
{
    /// <summary>
    /// Name or id of beer to removed
    /// </summary>
    [Required]
    public string Name { get; set; }
}