using System.ComponentModel.DataAnnotations;

namespace Brewery.BL.Client.Contracts.Inputs.Orders;

/// <summary>
/// Create order request
/// </summary>
public class EstimationOrderInput
{
    /// <summary>
    /// The names or ids of beers and quantities list 
    /// </summary>
    [Required]
    public IList<EstimationOrderItemInput> Beers { get; set; }
    
    /// <summary>
    /// Seller id
    /// </summary>
    [Required]
    public Guid WholesalerId { get; set; }
}




/// <summary>
/// Item information about a order of beer
/// </summary>
public class EstimationOrderItemInput
{
    /// <summary>
    /// Name or id of beer
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// Number of beers desired
    /// </summary>
    [Required, Range(1, Int32.MaxValue)]
    public int Count { get; set; }
}