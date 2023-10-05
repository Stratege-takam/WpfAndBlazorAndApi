using Elia.Core.Services.ServerRest;
namespace Elia.Core.Utils;

/// <summary>
/// 
/// </summary>
public abstract class FormatResult
{
    #region Properties (Private)
    
    /// <summary>
    /// This properties must save jwt param in appsetting
    /// </summary>
    private  readonly AppSettings.Server _appSettingsServer;
    
    #endregion
    
    
    #region Properties (Public)

    protected static string Token { get; set; }
    protected  string BaseUrl { get; set; }
  
    /// <summary>
    ///     <para>
    ///         This property represents the repository
    ///     </para>
    /// </summary>
    protected ServerRestService Http;
    
    
    #endregion

    
    #region Constructor

   
    public FormatResult(ServerRestService http, AppSettings.Server server)
    {
        Http = http;
        Http.Token = Token;
        BaseUrl = server.BaseUrlApi;
        _appSettingsServer = server;
    }

    #endregion
    
    public void SetToken(string token)
    {
        Token = token;
        Http.Token = token;
    }
    
    public string GetToken()
    {
        return Token;
    }
        /// <summary>
        /// Execute some business logic asynchronously and returns a consistent HttpResponseDto wich own the data to be returned along with some status.
        /// </summary>
        /// <typeparam name="TReturn">The type of the data returned by the Business Logic.</typeparam>
        /// <param name="blFunctionToExecute">The business logic function to execute.</param>
        /// <returns>A consistent HttpResponseDto filled with the returned data if any and the status.</returns>
        protected async Task<TReturn> ExecuteAsync<TReturn>(Func<Task<BaseResult<TReturn>>> blFunctionToExecute)
         where TReturn: BaseHttpResponse
        {

            // First executes the business logic function
            var result = await blFunctionToExecute();

            if (result.IsSuccess)
            {
                return result.Data;
            }

            return new BaseHttpResponse()
            {
                Reason = result.Reason,
                ResultStatus = result.Status
            } as TReturn;

        }

}