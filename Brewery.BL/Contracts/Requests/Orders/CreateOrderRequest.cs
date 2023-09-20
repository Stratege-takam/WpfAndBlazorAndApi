using System.ComponentModel.DataAnnotations;

namespace Brewery.BL.Contracts.Requests.Orders;

/// <summary>
/// Create order request
/// </summary>
public class CreateOrderRequest: EstimationOrderRequest
{
    /// <summary>
    /// The buyer id
    /// </summary>
    [Required]
    public Guid ClientId { get; set; }
}


