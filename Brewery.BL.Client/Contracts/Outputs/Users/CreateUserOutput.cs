namespace Brewery.BL.Contracts.Responses.Users;

/// <summary>
/// 
/// </summary>
public class CreateUserOutput
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Firstname { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public string Token { get; set; }
}