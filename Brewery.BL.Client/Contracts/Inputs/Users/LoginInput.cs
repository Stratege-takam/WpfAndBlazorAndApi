using System.ComponentModel.DataAnnotations;

namespace Brewery.BL.Client.Contracts.Inputs.Users;

/// <summary>
/// 
/// </summary>
public class LoginInput
{
    /// <summary>
    /// 
    /// </summary>
    [Required, EmailAddress]
    public string Email { get; set; }

    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string Password { get; set; }
}