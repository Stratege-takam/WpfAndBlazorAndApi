namespace Elia.Core.Utils;

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
    
    
    /// <summary>
    /// 
    /// </summary>
    public class Server
    {
        /// <summary>
        /// 
        /// </summary>
        public string BaseUrlApi { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string BaseUrlMedia { get; set; }
    }
}