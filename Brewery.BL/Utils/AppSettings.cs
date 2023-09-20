namespace Brewery.Bl.Utils;

/// <summary>
/// 
/// </summary>
public class AppSettings
{
    /// <summary>
    /// 
    /// </summary>
    public class Jwt
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Issuer { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
    }
}