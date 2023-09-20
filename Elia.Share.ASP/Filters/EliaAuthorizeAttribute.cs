using Elia.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Elia.Share.ASP.Filters;


/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public sealed class EliaAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        
        if (context.HttpContext.User.Identity?.IsAuthenticated != true)
        {
           
            // Microsoft.Identity.Web -> install
            context.Result = new UnauthorizedObjectResult(new BaseHttpResponse()
            {
                ResultStatus = BaseResultStatus.Unauthorized,
                Reason = "Not valid token"
            });
            
            return;
        }
        
        var token = context.HttpContext.Request.Headers.Authorization;
        
        // Decode token and check roles;

    }
}