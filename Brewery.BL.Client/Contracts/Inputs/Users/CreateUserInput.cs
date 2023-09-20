using System.ComponentModel.DataAnnotations;

namespace Brewery.BL.Client.Contracts.Inputs.Users;

/// <summary>
/// 
/// </summary>
public class CreateUserInput: LoginInput
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public string Firstname { get; set; }
    
}