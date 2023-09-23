using Brewery.BL.Client.Contracts.Outputs.Companies;
using Elia.Core.Attributes;
using Elia.Core.Enums;
using Elia.Core.Services.ServerRest;
using Elia.Core.Utils;
using Microsoft.Extensions.Options;

namespace Brewery.BL.Client.Business.Breweries;

    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management 
    ///         of Brewery.
    ///     </para>
    /// </summary>
    [Injectable()]
    public class BreweryService : FormatResult
    {
        #region Constructor

       
        public BreweryService(ServerRestService http, IOptions<AppSettings.Server> appSettingsClientSection): base(http, appSettingsClientSection.Value)
        {
        }

        #endregion
        
        
               #region Public methods

        /// <summary>
        ///     Payload a quote from get breweries
        /// </summary>
        /// <remarks>
        ///   ## Header
        ///   ``````````````````````````````````````````````````````
        ///     Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MjYxNTQwNTgsImlzcyI6IkJyZXdlcnlsb2NhbFNlY3JldEtleSIsImF1ZCI6ImJyZXdlcnkubG9jYWwuY29tIn0.0JSBKqXH1sB6voiZa6x2l1x6YI-Q1kA0xQG9lkH3vHs
        ///   ``````````````````````````````````````````````````````
        /// 
        ///     ## Example Request ##
        ///     ``````````````````````````````````````````````````````
        ///     Get /api/Brewery/Search/Beer 1
        ///     ``````````````````````````````````````````````````````
        ///     *get brewery.*
        /// 
        /// 
        ///     ## Example Response ##
        ///     ``````````````````````````````````````````````````````
        ///     When Reason exist, data is null. When Data exist, reason must be null
        ///      {
        ///        Data: {
        ///         "Results": [{"Id": "3fa85f64-5717-4562-b3fc-2c963f66afb1",
        ///         "Name": "Beer 1"}],
        ///         "Count": 1
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
        /// <param name="search">The query/param>
        /// <returns></returns>
        /// <param name="take">Count to get </param>
        /// <param name="skip"> count to skip</param>
        public  Task<BaseHttpResponse<ListResult<GetCompanyOutput>>> GetBreweriesAsync( string search, int take, int skip)
        {
            var route = string.IsNullOrEmpty(search)
                ? $"{BaseUrl}/Brewery/Search/${search}/${take}/${skip}"
                : $"{BaseUrl}/Brewery/Search/${take}/${skip}";
            
            return ExecuteAsync(() =>
                Http.RunAsync<BaseHttpResponse<ListResult<GetCompanyOutput>>>(route, Verb.GET));
        }


        #endregion


    }