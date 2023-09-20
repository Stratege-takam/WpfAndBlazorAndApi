namespace Brewery.BL.Contracts.Requests.Beers;

/// <summary>
/// 
/// </summary>
public class SearchBeerByWholesalerAndBreweryInput
{
    /// <summary>
    /// The names or ids of breweries 
    /// </summary>
    public IList<string> Breweries { get; set; }
    
    /// <summary>
    /// The names  or ids of wholesalers
    /// </summary>
    public IList<string> Wholesalers { get; set; }

    /// <summary>
    /// Skip result
    /// </summary>
    public int? Skip { get; set; }
    
    /// <summary>
    /// Count of list that you want
    /// </summary>
    public int? Take { get; set; }
}