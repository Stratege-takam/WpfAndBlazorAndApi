using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elia.Core.BaseModel;

namespace Brewery.BO.Entities;


/// <summary>
/// 
/// </summary>
[Table("StockBeerWholesalers")]
public class StockBeerWholesalerEntity: Track
{
    
    /// <summary>
    /// Beer sold
    /// </summary>
    public BeerEntity Beer { get; set; }

    /// <summary>
    /// Beer sold Id
    /// </summary>
    [Required]
    public Guid BeerId { get; set; }
    
    
    /// <summary>
    /// Wholesaler
    /// </summary>
    public WholesalerEntity Wholesaler { get; set; }

    /// <summary>
    /// Wholesaler  Id
    /// </summary>
    [Required]
    public Guid WholesalerId { get; set; }
    
    /// <summary>
    ///  Quantity of beer in stock at the wholesaler
    /// </summary>
    [Required]
    public int Stock { get; set; }

}