using System.ComponentModel.DataAnnotations;

namespace Brewery.BL.Client.Contracts.Inputs.Beers;


/// <summary>
/// 
/// </summary>
public class CreateBeerInput
{
    /// <summary>
    /// Name of beer
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    
    /// <summary>
    /// Price of beer
    /// </summary>
    [Required, Range(0.01, int.MaxValue)]
    public double Price { get; set; }

    
    /// <summary>
    /// Degree of beer
    /// </summary>
    [Required, Range(0, 100)]
    public double Degree { get; set; }
    
    
    /// <summary>
    /// Id of brewer 
    /// </summary>
    [Required]
    public Guid OwnerId { get; set; }

    /// <summary>
    /// Description of beer
    /// </summary>
    public string Description { get; set; }
}