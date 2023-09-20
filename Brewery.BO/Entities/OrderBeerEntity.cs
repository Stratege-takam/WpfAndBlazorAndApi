using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elia.Core.BaseModel;

namespace Brewery.BO.Entities;


/// <summary>
/// 
/// </summary>
[Table("OrderBeers")]
public class OrderBeerEntity: Track
{
    /// <summary>
    /// Beer that the wholesaler want
    /// </summary>
    public BeerEntity Beer { get; set; }
    
    /// <summary>
    /// Beer Id that the wholesaler want
    /// </summary>
    [Required]
    public Guid BeerId { get; set; }
    
    
    /// <summary>
    /// Matching command
    /// </summary>
    public OrderEntity Order { get; set; }
    
    /// <summary>
    /// Matching command id
    /// </summary>
    [Required]
    public Guid OrderId { get; set; }

    /// <summary>
    /// Number of beers desired
    /// </summary>
    [Required]
    public int Count { get; set; }
}