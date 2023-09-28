using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Elia.Core.BaseModel;

namespace Brewery.BO.Entities;


/// <summary>
/// 
/// </summary>
[Table("Medias")]
public class MediaEntity: Track
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Hashname { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Extension { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string SubDir { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public double Size { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public ObservableCollection<BeerEntity> Beers { get; set; }
    
}