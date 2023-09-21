using Elia.Core.Utils;

namespace Elia.Core.Extensions;

/// <summary>
/// 
/// </summary>
public static class TryFunc
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="blFunctionToExecute"></param>
    /// <typeparam name="TReturn"></typeparam>
    /// <returns></returns>
    public static async Task<BaseResult<TReturn>> ExecuteAsync<TReturn>(Func<Task<BaseResult<TReturn>>> blFunctionToExecute)
    {
        try
        {
            var result = await blFunctionToExecute();
                
            return new BaseResult<TReturn>(result);
        }
        catch (Exception e)
        {
            return new BaseResult<TReturn>(BaseResultStatus.Failure, e);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="blFunctionToExecute"></param>
    /// <typeparam name="TReturn"></typeparam>
    /// <returns></returns>
    public static  BaseResult<TReturn> Execute<TReturn>(Func<BaseResult<TReturn>> blFunctionToExecute)
    {
        try
        {
            var result =  blFunctionToExecute();
                
            return new BaseResult<TReturn>(result);
        }
        catch (Exception e)
        {
            return new BaseResult<TReturn>(BaseResultStatus.Failure, e);
        }
    }

}