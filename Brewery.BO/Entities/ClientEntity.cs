using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elia.Core.BaseModel;

namespace Brewery.BO.Entities;


/// <summary>
/// 
/// </summary>
[Table("Clients")]
public class ClientEntity: Track
{
    /// <summary>
    /// Name of client
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// Address of client
    /// </summary>
    public string Address { get; set; }
}