using Brewery.BL.Business.Breweries;
using Brewery.Contract.Contracts.Responses.Companies;
using Elia.Core.Utils;
using Elia.Share.ASP.Filters;
using Microsoft.AspNetCore.Mvc;
namespace Brewery.API.Controllers;



     /// <summary>
    /// User Controller class
    /// </summary>
    [Route("api/[controller]"), 
     EliaAuthorize, 
     ApiController]
    public class BreweryController : BaseController<BreweryController>
    {
        #region Properties (Private)

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<BreweryController> _logger;

        /// <summary>
        /// Represents the business logic of the beer that will be injected in this controller
        /// </summary>
        private readonly BreweryBl _bl;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="bl">Represents the injection of the CommentLikes business logic</param>
        public BreweryController(ILogger<BreweryController> logger, BreweryBl bl) : base(logger)
        {
            _logger = logger;
            _bl = bl;
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
        [HttpGet("Search/{search}/{take}/{skip}"),  HttpGet("Search/{take}/{skip}")]
        public  Task<BaseHttpResponse<ListResult<GetCompanyResponse>>> GetBreweriesAsync( string search, int take, int skip)
            => ExecuteBlAsync(() => _bl.GetBreweriesAsync(search, skip, take ));

        #endregion
    }