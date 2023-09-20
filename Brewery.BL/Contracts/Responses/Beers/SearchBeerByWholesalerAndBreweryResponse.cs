namespace Brewery.BL.Contracts.Responses.Beers;

/// <summary>
/// response of search beer
/// </summary>
public class SearchBeerByWholesalerAndBreweryResponse: CreateBeerResponse
{
    /// <summary>
    /// Information about brewery
    /// </summary>
    public BreweryCreateBeerResponse Owner { get; set; }
}


/// <summary>
/// Information about brewery
/// </summary>
/// <param name="Name">Name of brewery</param>
/// <param name="Id">Id of brewery</param>
public record BreweryCreateBeerResponse(string Name, Guid Id);