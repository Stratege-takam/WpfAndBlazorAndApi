using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elia.Core.BaseModel;

namespace Brewery.BO.Entities;

[Table("Users")]
public class UserEntity: Track
{
    [Required, EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Firstname { get; set; }
    
    [Required]
    public string Password { get; set; }

    [Required]
    public string Token { get; set; }
}