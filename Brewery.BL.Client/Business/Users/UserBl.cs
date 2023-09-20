using Brewery.BL.Client.Contracts.Inputs.Users;
using Brewery.BL.Contracts.Responses.Users;
using Elia.Core.Attributes;
using Elia.Core.Enums;
using Elia.Core.Extensions;
using Elia.Core.Services.ServerRest;
using Elia.Core.Utils;
using Microsoft.Extensions.Options;
using AppSettings = Elia.Core.Utils.AppSettings;

namespace Brewery.BL.Client.Business.Users;

    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management 
    ///         of Beer.
    ///     </para>
    /// </summary>
    [Injectable()]
    public class UserBl : FormatResult
    {
        #region Constructor

       
        public UserBl(ServerRestService http, IOptions<AppSettings.Server> appSettingsClientSection): base(http, appSettingsClientSection.Value)
        {
        }

        #endregion
        
        

        #region Public methods

        
        /// <summary>
        ///     Payload a quote from create user
        /// </summary>
        /// <remarks>
        /// 
        ///     ## Example Payload ##
        ///     ``````````````````````````````````````````````````````
        ///     POST /api/User
        ///     {
        ///         "Email": "danick.takam@test.be",
        ///         "Firstname": "Danick",
        ///         "Password": "MyPasswd",
        ///     }
        ///     ``````````````````````````````````````````````````````
        ///     *Create new user.*
        ///
        ///
        ///     ## Example Response ##
        ///     ``````````````````````````````````````````````````````
        ///     When Reason exist, data is null. When Data exist, reason must be null
        ///      {
        ///        Data: {
        ///         "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "Email": "danick.takam@test.be",
        ///         "Firstname": "Danik",
        ///         "Token": "rbznpGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKaWduZXNoIFRyaXZlZGkiLCJlbWFpbCI6InRlc3QuYnRlc3RAZ21haWwuY29tIiwiRGF0ZU9mSm9pbmciOiIwMDAxLTAxLTAxIiwianRpIjoiYzJkNTZjNzQtZTc3Yy00ZmUxLTgyYzAtMzlhYjhmNzFmYzUzIiwiZXhwIjoxNTMyMzU2NjY5LCJpc3MiOiJUZXN0LmNvbSIsImF1ZCI6IlRlc3QuY29tIn0.8hwQ3H9V8mdNYrFZSjbCpWSyR1CNyDYHcdf6GqqCGnY",
        ///        },
        ///        Reason: "Some error if error",
        ///        ResultStatus: "SUCCESS"
        ///     }
        ///     
        ///     ``````````````````````````````````````````````````````
        /// </remarks>
        /// <response code="200">
        ///     **Data** - The beer has been create.
        ///     **Unexcepted** - An unexecepted error occurs.
        /// </response>
        /// <param name="request">The model that allow to ask estimation</param>
        /// <returns></returns>
        public  Task<BaseHttpResponse<CreateUserOutput>> CreateuserAsync(CreateUserInput request)
            => ExecuteAsync(() => Http.RunAsync<BaseHttpResponse<CreateUserOutput>>($"{BaseUrl}/User", Verb.POST, request));
        
        
        
        /// <summary>
        ///     Payload a quote from login user
        /// </summary>
        /// <remarks>
        /// 
        ///     ## Example Payload ##
        ///     ``````````````````````````````````````````````````````
        ///     POST /api/User/Login
        ///     {
        ///         "Email": "danick.takam@test.be",
        ///         "Password": "MyPasswd",
        ///     }
        ///     ``````````````````````````````````````````````````````
        ///     *Login user.*
        ///
        ///
        ///     ## Example Response ##
        ///     ``````````````````````````````````````````````````````
        ///     When Reason exist, data is null. When Data exist, reason must be null
        ///      {
        ///        Data: {
        ///         "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "Email": "danick.takam@test.be",
        ///         "Firstname": "Danik",
        ///         "Token": "rbznpGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKaWduZXNoIFRyaXZlZGkiLCJlbWFpbCI6InRlc3QuYnRlc3RAZ21haWwuY29tIiwiRGF0ZU9mSm9pbmciOiIwMDAxLTAxLTAxIiwianRpIjoiYzJkNTZjNzQtZTc3Yy00ZmUxLTgyYzAtMzlhYjhmNzFmYzUzIiwiZXhwIjoxNTMyMzU2NjY5LCJpc3MiOiJUZXN0LmNvbSIsImF1ZCI6IlRlc3QuY29tIn0.8hwQ3H9V8mdNYrFZSjbCpWSyR1CNyDYHcdf6GqqCGnY",
        ///        },
        ///        Reason: "Some error if error",
        ///        ResultStatus: "SUCCESS"
        ///     }
        ///     
        ///     ``````````````````````````````````````````````````````
        /// </remarks>
        /// <response code="200">
        ///     **Data** - The beer has been create.
        ///     **Unexcepted** - An unexecepted error occurs.
        /// </response>
        /// <param name="request">The model that allow to ask estimation</param>
        /// <returns></returns>
        public  Task<BaseHttpResponse<CreateUserOutput>> LoginAsync( LoginInput request)
            => ExecuteAsync(() => Http.RunAsync<BaseHttpResponse<CreateUserOutput>>($"{BaseUrl}/User/Login", Verb.POST, request));
        
    
        #endregion

    }