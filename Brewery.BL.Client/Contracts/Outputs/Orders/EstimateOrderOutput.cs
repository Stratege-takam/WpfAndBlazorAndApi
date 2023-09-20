namespace Brewery.BL.Client.Contracts.Outputs.Orders;

/// <summary>
/// Devis response
/// </summary>
public class EstimateOrderOutput
{
    /// <summary>
    /// The names or ids of beers and quantities list 
    /// </summary>
    public IList<EstimateOrderItemOutput> Beers { get; set; }

    /// <summary>
    /// Total 
    /// </summary>
    public double Total {
        get => Beers?.Sum(b => b.Total) ?? 0;
    }

    /// <summary>
    /// Total to by
    /// </summary>
    public double TotalToPay => Total - DiscountAmount;
    
    /// <summary>
    /// Discount 10% when count beer > 10 and 20% when count beer > 20
    /// </summary>
    public double Discount
    {
        get
        {
            if (Beers != null)
            {
                var count = Beers.Sum(b => b.Count);
                
                return count > 20 ? 0.2 : count > 10 ? 0.1 : 0;
            }

            return 0;
        }
    }

    /// <summary>
    /// Discount amount
    /// </summary>
    public double DiscountAmount => Total * Discount;

}


/// <summary>
/// Item information about a devis of beer
/// </summary>
/// <param name="Name">Name  of beer</param>
/// <param name="Id">Id of beer</param>
/// <param name="Price">Price of beer</param>
/// <param name="Count">Number of beers desired</param>
public record EstimateOrderItemOutput(string Name, Guid? Id, int Count, double Price)
{
    /// <summary>
    /// Total
    /// </summary>
    public double Total
    {
        get => Count * Price;
    }
};
