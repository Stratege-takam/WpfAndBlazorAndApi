using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elia.Core.BaseModel;

namespace Brewery.BO.Entities;

/// <summary>
/// 
/// </summary>
[Table("Orders")]
public class OrderEntity: Track
{
    /// <summary>
    /// List of beer desired
    /// </summary>
    public ICollection<OrderBeerEntity> OrderBeers { get; set; }

    /// <summary>
    /// Order number
    /// </summary>
    [Required]
    public string CommandNumber { get; set; }

    /// <summary>
    /// Client
    /// </summary>
    public ClientEntity Client { get; set; }

    /// <summary>
    /// Client Id
    /// </summary>
    [Required]
    public Guid ClientId { get; set; }
    
    
    /// <summary>
    /// Wholesaler
    /// </summary>
    public WholesalerEntity Wholesaler { get; set; }

    /// <summary>
    /// Wholesaler Id
    /// </summary>
    [Required]
    public Guid WholesalerId { get; set; }


    /// <summary>
    /// Sale status
    /// </summary>
    public bool IsSold { get; set; }
    
}