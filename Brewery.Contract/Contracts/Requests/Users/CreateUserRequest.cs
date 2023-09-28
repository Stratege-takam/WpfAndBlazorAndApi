using System.ComponentModel.DataAnnotations;

namespace Brewery.Contract.Contracts.Requests.Users;

/// <summary>
/// 
/// </summary>
public class CreateUserRequest: LoginRequest
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string Firstname { get; set; }
    
}