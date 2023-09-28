using Elia.Core.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Elia.Share.ASP.Middlewares;

/// <summary>
/// 
/// </summary>
public class NotFoundMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    private readonly RequestDelegate _next;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
    public NotFoundMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        if (context.Response.StatusCode == 404)
        {
            context.Response.ContentType =  context.Response.ContentType  ?? "application/json";
            var res = new BaseHttpResponse()
            {
                Reason = "The requested resource is not found...",
                ResultStatus = BaseResultStatus.NotFound
            };
            var json = JsonConvert.SerializeObject(res);
            await context.Response.WriteAsync(json);            }
    }
}