using System.ComponentModel.DataAnnotations.Schema;

namespace Brewery.BO.Entities;


/// <summary>
/// 
/// </summary>
[Table("Breweries")]
public class BreweryEntity: CompanyEntity
{
    /// <summary>
    /// LList of beers sold by the brewer
    /// </summary>
    public ICollection<BeerEntity> Beers { get; set; }
}