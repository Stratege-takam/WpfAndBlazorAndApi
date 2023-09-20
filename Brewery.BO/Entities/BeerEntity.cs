using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elia.Core.BaseModel;

namespace Brewery.BO.Entities;


/// <summary>
/// 
/// </summary>
[Table("Beers")]
public class BeerEntity: Track
{
    /// <summary>
    /// 
    /// </summary>
    public Guid MediaId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public MediaEntity Media { get; set; }
    
    /// <summary>
    /// Name of beer
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Degree of alcohol
    /// </summary>
    [Required, Range(0, 100)]
    public double Degree { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public string DegreePercent
    {
        get => $"{Degree}%";
    }

    /// <summary>
    /// Price of beer
    /// </summary>
    [Required, Range(0.01, Int32.MaxValue)]
    public double Price { get; set; }


    /// <summary>
    /// Owner of beer
    /// </summary>
    public BreweryEntity Owner { get; set; }
    
    /// <summary>
    /// Owner id of beer
    /// </summary>
    [Required]
    public Guid OwnerId { get; set; }


    /// <summary>
    /// List of Wholesalers
    /// </summary>
    public ICollection<StockBeerWholesalerEntity> StockBeerWholesalers  { get; set; }

}