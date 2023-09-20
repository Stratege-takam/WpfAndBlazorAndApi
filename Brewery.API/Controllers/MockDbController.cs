using Brewery.BL.Business.Mock;
using Brewery.BL.Contracts.Requests.Mock;
using Brewery.BL.Contracts.Responses.Mock;
using Elia.Core.Utils;
using Microsoft.AspNetCore.Mvc;
namespace Brewery.API.Controllers;


     /// <summary>
    /// BeerController class
    /// </summary>
    [Route("api/[controller]"), 
     ApiController]
    public class MockDbController : BaseController<MockDbController>
    {
        #region Properties (Private)

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<MockDbController> _logger;

        /// <summary>
        /// Represents the business logic of the beer that will be injected in this controller
        /// </summary>
        private readonly MockDbBl _bl;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="bl">Represents the injection of the CommentLikes business logic</param>
        public MockDbController(ILogger<MockDbController> logger, MockDbBl bl) : base(logger)
        {
            _logger = logger;
            _bl = bl;
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
        [HttpPost("Seed")]
        public  Task<BaseHttpResponse<MockResponse>> RunSeedAsync([FromBody] MockRequest request)
            => ExecuteBlAsync(() => _bl.RunSeedAsync(request));
        
        
        
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
        [HttpPost("Clean")]
        public  Task<BaseHttpResponse<MockResponse>> CleanTablesAsync([FromBody] MockRequest request)
            => ExecuteBlAsync(() => _bl.CleanTablesAsync(request));

        #endregion

    }