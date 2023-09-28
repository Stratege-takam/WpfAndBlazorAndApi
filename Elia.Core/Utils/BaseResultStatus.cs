using System.ComponentModel;
namespace Elia.Core.Utils;


/// <summary>
/// 
/// </summary>
public enum BaseResultStatus
{
    /// <summary>
    /// 
    /// </summary>
    [Description("NONE")]
    None = 1,
    /// <summary>
    /// 
    /// </summary>
    [Description("SUCCESS")]
    Success = 2,
    /// <summary>
    /// 
    /// </summary>
    [Description("SUCCESSWITHWARNING")]
    SuccessWithWarnings = 3,
    /// <summary>
    /// 
    /// </summary>
    [Description("FAILURE")]
    Failure = 4,
    /// <summary>
    /// 
    /// </summary>
    [Description("UNEXPECTED")]
    Unexpected = 5,
    /// <summary>
    /// 
    /// </summary>
    [Description("UNAUTHORIZED")]
    Unauthorized = 6,
    /// <summary>
    /// 
    /// </summary>
    [Description("ALREADY")]
    Already = 7,
    /// <summary>
    /// 
    /// </summary>
    [Description("NOTFOUND")]
    NotFound = 8,
    /// <summary>
    /// 
    /// </summary>
    [Description("BADPARAMS")]
    BadParams = 9,
    /// <summary>
    /// 
    /// </summary>
    [Description("TIMEOUT")]
    Timeout = 10,
    /// <summary>
    /// 
    /// </summary>
    [Description("NOCONNECTION")]
    NoConnection = 11,
    /// <summary>
    /// 
    /// </summary>
    [Description("NOTIMPLEMENT")]
    NotImplement = 12,
    /// <summary>
    /// 
    /// </summary>
    [Description("CANCEL")]
    Cancel = 13
}