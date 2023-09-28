
namespace Elia.Core.Utils;


/// <summary>
/// 
/// </summary>
public class BaseHttpResponse
{
    /// <summary>
    /// 
    /// </summary>
    public BaseResultStatus ResultStatus { get; set; }

    /// <summary>
    /// The reason when error
    /// </summary>
    public string Reason { get; set; }
        
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public class BaseHttpResponse<TResponse> : BaseHttpResponse
{
    /// <summary>
    /// The data that we expose to the client
    /// </summary>
    public TResponse Data { get; set; }
}