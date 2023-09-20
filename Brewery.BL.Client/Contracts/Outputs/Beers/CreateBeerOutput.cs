namespace Brewery.BL.Client.Contracts.Outputs.Beers;

/// <summary>
/// 
/// </summary>
public class CreateBeerOutput
{
    /// <summary>
    /// Id of beer
    /// </summary>
    public Guid Id { get; set; }
    
    
    /// <summary>
    /// Name of beer
    /// </summary>
    public string Name { get; set; }
    
    
    /// <summary>
    /// Price of beer
    /// </summary>
    public double Price { get; set; }

    
    /// <summary>
    /// Degree of beer
    /// </summary>
    public string Degree { get; set; }
}

