using Brewery.Contract.Contracts.Requests.Mock;
using Brewery.Contract.Contracts.Responses.Mock;
using Elia.Core.Attributes;
using Elia.Core.Enums;
using Elia.Core.Services.ServerRest;
using Elia.Core.Utils;
using Microsoft.Extensions.Options;
using AppSettings = Elia.Core.Utils.AppSettings;

namespace Brewery.Services.Services.Mock;

    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management mock db 
    ///     </para>
    /// </summary>
    [Injectable()]
    public class MockDbService: FormatResult
    {
        #region Constructor

       
        public MockDbService(ServerRestService http, IOptions<AppSettings.Server> appSettingsClientSection): base(http, appSettingsClientSection.Value)
        {
        }

        #endregion
        
        #region Actions


        /// <summary>
        ///     Seed db
        /// </summary>
        /// <remarks>
        ///     ## Example Request ##
        ///     ``````````````````````````````````````````````````````
        ///     POST /api/MockDb/Seed
        ///     {
        ///         "Email": "danick.takam@gbrewery.be",
        ///         "Password": "MyPassword"
        ///     }
        ///     ``````````````````````````````````````````````````````
        ///     *seed db.*
        ///
        ///     ## Example Response ##
        ///     ``````````````````````````````````````````````````````
        ///     When Reason exist, data is null. When Data exist, reason must be null
        ///      {
        ///        Reason: "Some error if error",
        ///        ResultStatus: "SUCCESS",
        ///        Data: true,
        ///     }
        ///     
        ///     ``````````````````````````````````````````````````````
        /// </remarks>
        /// <response code="200">
        ///     **Unexcepted** - An unexecepted error occurs.
        /// </response>
        /// <param name="request">Information to auth user</param>
        /// <returns></returns>
        public  Task<BaseHttpResponse<MockResponse>> RunSeedAsync( MockRequest request)
            => ExecuteAsync(() => Http.RunAsync<BaseHttpResponse<MockResponse>>($"{BaseUrl}/MockDb/Seed", Verb.POST, request));
        
        
        
        /// <summary>
        ///     Clean db
        /// </summary>
        /// <remarks>
        /// 
        ///     ## Example Request ##
        ///     ``````````````````````````````````````````````````````
        ///     POST /api/MockDb/Clean
        ///     {
        ///         "Email": "danick.takam@gbrewery.be",
        ///         "Password": "MyPassword"
        ///     }
        ///     ``````````````````````````````````````````````````````
        ///     *clean db.*
        ///
        ///     ## Example Response ##
        ///     ``````````````````````````````````````````````````````
        ///     When Reason exist, data is null. When Data exist, reason must be null
        ///      {
        ///        Reason: "Some error if error",
        ///        ResultStatus: "SUCCESS",
        ///        Data: true,
        ///     }
        ///     
        ///     ``````````````````````````````````````````````````````
        /// </remarks>
        /// <response code="200">
        ///     **Unexcepted** - An unexecepted error occurs.
        /// </response>
        /// <param name="request">Information to auth user</param>
        /// <returns></returns>
        public  Task<BaseHttpResponse<MockResponse>> CleanTablesAsync( MockRequest request)
            => ExecuteAsync(() => Http.RunAsync<BaseHttpResponse<MockResponse>>($"{BaseUrl}/MockDb/Clean", Verb.POST, request));

        #endregion

    }