using Elia.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Elia.Share.ASP.Filters;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public class ValidationModelAttribute: ActionFilterAttribute
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        var objectResult = context.Result as ObjectResult;
        var resultValue = objectResult.Value;
        if(!context.ModelState.IsValid && !(resultValue is BaseHttpResponse))
        {
          //  var response = context.HttpContext.Response;
          //  var status = context.HttpContext.Response.StatusCode;
            
            var result = resultValue as ValidationProblemDetails;
            var errors = JsonConvert.SerializeObject(result.Errors);
            
            var res = new BaseHttpResponse()
            {
                Reason = errors,
                ResultStatus = BaseResultStatus.BadParams
            };

            context.Result = new BadRequestObjectResult(res);

        }
        base.OnResultExecuting(context);
    }
}