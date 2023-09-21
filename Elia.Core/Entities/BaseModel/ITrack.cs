namespace Elia.Core.BaseModel;

/// <summary>
/// 
/// </summary>
public interface ITrack
{
    /// <summary>
    /// 
    /// </summary>
    Guid Id{get; set;}
    
    /// <summary>
    /// 
    /// </summary>
    DateTime? CreateAt{get; set;}
    
    /// <summary>
    /// 
    /// </summary>
    DateTime? UpdateAt{get; set;} 
    
    /// <summary>
    /// 
    /// </summary>
    DateTime? DeleteAt{get; set;} 
}