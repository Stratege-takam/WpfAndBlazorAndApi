namespace Brewery.BL.Client.Contracts.Outputs.Beers;

/// <summary>
/// response of search beer
/// </summary>
public class SearchBeerByWholesalerAndBreweryOutput: CreateBeerOutput
{
    /// <summary>
    /// Information about brewery
    /// </summary>
    public BreweryCreateBeerOutput Owner { get; set; }
}


/// <summary>
/// Information about brewery
/// </summary>
/// <param name="Name">Name of brewery</param>
/// <param name="Id">Id of brewery</param>
public record BreweryCreateBeerOutput(string Name, Guid Id);