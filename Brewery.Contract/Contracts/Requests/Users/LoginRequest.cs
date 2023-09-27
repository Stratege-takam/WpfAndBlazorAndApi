using System.ComponentModel.DataAnnotations;

namespace Brewery.BL.Contracts.Requests.Users;

/// <summary>
/// 
/// </summary>
public class LoginRequest
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