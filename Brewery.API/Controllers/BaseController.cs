
using System.Diagnostics;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Elia.Share.ASP.Filters;
using Microsoft.AspNetCore.Mvc;
using Environments = Elia.Core.Utils.Environments;

namespace Brewery.API.Controllers;


    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TController"></typeparam>
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [ValidationModel]

    public abstract partial  class BaseController<TController>: ControllerBase
    {
        
        /// <summary>
        /// 
        /// </summary>
        public  bool IsDevEnv
        {
            get => Environments.Current != EnvironmentEnum.Prod;
        }

        private readonly ILogger<TController> _logger;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public BaseController(ILogger<TController> logger)
        {
            _logger = logger;
        }
        
        
              #region Methods (Protected)

        /// <summary>
        /// Execute some business logic asynchronously and returns a consistent HttpResponseDto wich own the data to be returned along with some status.
        /// </summary>
        /// <typeparam name="TReturn">The type of the data returned by the Business Logic.</typeparam>
        /// <param name="blFunctionToExecute">The business logic function to execute.</param>
        /// <returns>A consistent HttpResponseDto filled with the returned data if any and the status.</returns>
        protected async Task<BaseHttpResponse<TReturn>> ExecuteBlAsync<TReturn>(Func<Task<BaseResult<TReturn>>> blFunctionToExecute)
        {
            try
            {
                // First executes the business logic function
                var result = await blFunctionToExecute();

                // If the result of the business logic is unexpected
                if (result.IsUnexpected)
                {
                    Debug.WriteLine($"[ControllerBase]: {result.Reason ?? result.Exception?.Message}");
                    _logger.LogError(result.Exception, $"[ControllerBase]{result.Reason ?? result.Exception?.Message}");
                }


                // Creates a response using the business logic function result
                var response = CreateHttpResponseBlFromResult(result);
                return response;
            }
            catch (Exception exception)
            {
                // If an unexpected error occurs then returns an unexpected response (should not occur as BL are included in a try/catch)
                Debug.WriteLine($"[ControllerBase]: Unexpected error when executing Business logic from controller. { exception.Message}");
                _logger.LogCritical("[ControllerBase]Unexpected error when executing Business logic from controller.", exception);

                return new BaseHttpResponse<TReturn>
                {
                    ResultStatus = BaseResultStatus.Unexpected,
                    Reason = exception.Message
                };
            }
        }

        /// <summary>
        /// Execute some business logic asynchronously and returns a consistent HttpResponseDto wich own the data to be returned along with some status.
        /// </summary>
        /// <param name="blFunctionToExecute">The business logic function to execute.</param>
        /// <returns>A consistent HttpResponseDto filled with the returned data if any and the status.</returns>
        protected async Task<BaseHttpResponse> ExecuteBlAsync(Func<Task<BaseResult>> blFunctionToExecute)
        {
            try
            {
                // First executes the business logic function
                var result = await blFunctionToExecute();

                // If the result of the business logic is unexpected
                if (result.IsUnexpected)
                    Debug.WriteLine($"[ControllerBase]: {result.Reason ?? result.Exception?.Message}");

                // Creates a response using the business logic function result
                var response = CreateHttpResponseBlFromResult(result);
                return response;
            }
            catch (Exception exception)
            {
                // If an unexpected error occurs then returns an unexpected response (should not occur as BL are included in a try/catch)
                Debug.WriteLine($"[ControllerBase]: Unexpected error when executing Business logic from controller. { exception.Message}");
                _logger.LogCritical("[ControllerBase]Unexpected error when executing Business logic from controller.", exception);

                return new BaseHttpResponse
                {
                    ResultStatus = BaseResultStatus.Unexpected,
                    Reason = exception.Message
                };
            }
        }

        /// <summary>
        /// Execute some business logic and returns a consistent HttpResponseDto wich own the data to be returned along with some status.
        /// </summary>
        /// <param name="blFunctionToExecute">The business logic function to execute.</param>
        /// <returns></returns>
        protected BaseHttpResponse ExecuteBl(Func<BaseResult> blFunctionToExecute)
            => ExecuteBlAsync(() => Task.Run(blFunctionToExecute)).Result;

        /// <summary>
        /// Execute some business logic and returns a consistent HttpResponseDto wich own the data to be returned along with some status.
        /// </summary>
        /// <typeparam name="TReturn">The type of the data returned by the Business Logic.</typeparam>
        /// <param name="blFunctionToExecute">The business logic function to execute.</param>
        /// <returns></returns>
        protected BaseHttpResponse<TReturn> ExecuteBl<TReturn>(Func<BaseResult<TReturn>> blFunctionToExecute)
            => ExecuteBlAsync(() => Task.Run(blFunctionToExecute)).Result;

        /// <summary>
        /// Creates a new HttpResponseDto given a Result{TResult}.
        /// </summary>
        /// <param name="result">The result to convert in HttpResponseDto.</param>
        /// <returns>The newly created HttpResponseDto.</returns>
        protected BaseHttpResponse CreateHttpResponseBlFromResult(BaseResult result)
            => new BaseHttpResponse
            {
                ResultStatus = result.Status,
                Reason = CreateReason(result.Exception) ?? result.Reason
            };

        /// <summary>
        /// Creates a new HttpResponseDto given a Result{TResult}.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The result to convert in HttpResponseDto.</param>
        /// <returns>The newly created HttpResponseDto.</returns>
        protected BaseHttpResponse<TResult> CreateHttpResponseBlFromResult<TResult>(BaseResult<TResult> result)
            => new BaseHttpResponse<TResult>
            {
                Data = result.Data,
                ResultStatus = result.Status,
                Reason = CreateReason(result.Exception) ?? result.Reason
            };

        /// <summary>
        /// Creates a reason from an exception.
        /// Depending on the environment gives some details.
        /// </summary>
        /// <param name="exception">The exception used to create the reason text.</param>
        /// <returns>The exception casted as text if environment allows it.</returns>
        protected string CreateReason(Exception exception)
        {
            // If no exception then no reason
            if (exception == null)
                return null;

            // No detail in Acceptance or Production
            if (!IsDevEnv)
                return null;

            // Returns the full detail of the exception in order to ease the debug
            return exception.InnerException != null ? exception.InnerException.Message  : exception.Message;
        }

        #endregion Methods (Protected)
    }