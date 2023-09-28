using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elia.Core.BaseModel;

namespace Brewery.BO.Entities;


/// <summary>
/// 
/// </summary>
[Table("Companies")]
public class CompanyEntity: Track
{
    /// <summary>
    /// Name of company
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// VAT number of company
    /// </summary>
    public string Vat { get; set; }

    /// <summary>
    /// Email of company
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Phone number of company
    /// </summary>
    public string Phone { get; set; }
    
    
    /// <summary>
    /// Address of company
    /// </summary>
    public string Address { get; set; }
}