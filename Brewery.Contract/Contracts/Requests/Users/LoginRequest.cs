using System.ComponentModel.DataAnnotations;

namespace Brewery.Contract.Contracts.Requests.Users;

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
    [Required, MinLength(4)]
    public string Password { get; set; }
}