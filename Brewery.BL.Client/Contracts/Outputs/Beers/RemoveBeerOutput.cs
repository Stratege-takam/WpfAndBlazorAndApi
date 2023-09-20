namespace Brewery.BL.Client.Contracts.Outputs.Beers;

/// <summary>
/// Response of removed beer
/// </summary>
public class RemoveBeerOutput
{
    /// <summary>
    /// Id of removed beer
    /// </summary>
    public Guid Id { get; set; }
}