using System.ComponentModel.DataAnnotations;

namespace Brewery.BL.Client.Contracts.Inputs.Orders;

/// <summary>
/// Create order request
/// </summary>
public class CreateOrderInput: EstimationOrderInput
{
    /// <summary>
    /// The buyer id
    /// </summary>
    [Required]
    public Guid ClientId { get; set; }
}


