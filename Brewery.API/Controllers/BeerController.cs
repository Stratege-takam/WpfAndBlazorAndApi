using Brewery.BL.Business.Beers;
using Brewery.Contract.Contracts.Requests.Beers;
using Brewery.Contract.Contracts.Responses.Beers;
using Elia.Core.Utils;
using Elia.Share.ASP.Filters;
using Microsoft.AspNetCore.Mvc;
namespace Brewery.API.Controllers;



     /// <summary>
    /// BeerController class
    /// </summary>
    [Route("api/[controller]"), 
     ApiController,
     EliaAuthorize]
    public class BeerController : BaseController<BeerController>
    {
        #region Properties (Private)

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<BeerController> _logger;

        /// <summary>
        /// Represents the business logic of the beer that will be injected in this controller
        /// </summary>
        private readonly BeerBl _bl;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="bl">Represents the injection of the CommentLikes business logic</param>
        public BeerController(ILogger<BeerController> logger, BeerBl bl) : base(logger)
        {
            _logger = logger;
            _bl = bl;
        }
        #endregion


        #region Actions

        /// <summary>
        ///     Creation of a new beer
        /// </summary>
        /// <remarks>
        ///   ## Header
        ///   ``````````````````````````````````````````````````````
        ///     Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MjYxNTQwNTgsImlzcyI6IkJyZXdlcnlsb2NhbFNlY3JldEtleSIsImF1ZCI6ImJyZXdlcnkubG9jYWwuY29tIn0.0JSBKqXH1sB6voiZa6x2l1x6YI-Q1kA0xQG9lkH3vHs
        ///   ``````````````````````````````````````````````````````
        /// 
        ///     ## Example Request ##
        ///     ``````````````````````````````````````````````````````
        ///     POST /api/Beer
        ///     {
        ///         "OwnerId": "08d9c2d4-bd3b-4129-8153-4924870c9cb7",
        ///         "Name": "Leffe Blonde",
        ///         "Description": "A description",
        ///         "Price": 2.20,
        ///         "Degree": 6.6
        ///     }
        ///     ``````````````````````````````````````````````````````
        ///     *Create beer.*
        ///
        ///
        ///     ## Example Response ##
        ///     ``````````````````````````````````````````````````````
        ///     When Reason exist, data is null. When Data exist, reason must be null
        ///      {
        ///        Data: {
        ///         "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "Name": "Leffe Blonde",
        ///         "Price": 2.20,
        ///         "Description": "A description",
        ///         "Degree": "6.6%"
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
        /// <param name="request">The model that allow to create beer</param>
        /// <returns></returns>
        [HttpPost]
        public  Task<BaseHttpResponse<CreateBeerResponse>> PostAsync([FromBody] CreateBeerRequest request)
            => ExecuteBlAsync(() => _bl.CreateBeerAsync(request));

        
        
        
        
        /// <summary>
        ///     Delete a beer from a brewer
        /// </summary>
        /// <remarks>
        ///   ## Header
        ///   ``````````````````````````````````````````````````````
        ///     Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MjYxNTQwNTgsImlzcyI6IkJyZXdlcnlsb2NhbFNlY3JldEtleSIsImF1ZCI6ImJyZXdlcnkubG9jYWwuY29tIn0.0JSBKqXH1sB6voiZa6x2l1x6YI-Q1kA0xQG9lkH3vHs
        ///   ``````````````````````````````````````````````````````
        /// 
        ///     ## Example Request ##
        ///     ``````````````````````````````````````````````````````
        ///     #Remove by name
        /// 
        ///     POST /api/Beer/Remove
        ///     {
        ///         "Name": "Leffe Blonde"
        ///     }
        ///     ``````````````````````````````````````````````````````
        ///
        ///     ``````````````````````````````````````````````````````
        ///
        ///     #Remove by id
        /// 
        ///     POST /api/Beer/Remove
        ///     {
        ///         "Name": "08d9c1d4-bd3b-5129-8153-4924870c9cb7"
        ///     }
        ///     ``````````````````````````````````````````````````````
        ///     *Remove beer.*
        ///
        ///
        ///     ## Example Response ##
        ///     ``````````````````````````````````````````````````````
        ///     When Reason exist, data is null. When Data exist, reason must be null
        /// 
        ///     {
        ///       Reason: "Some reason",
        ///       ResultStatus: "SUCCESS",
        ///        Data: {
        ///         "Id": "08d9c1d4-bd3b-5129-8153-4924870c9cb7",
        ///       }
        ///     }
        ///
        ///     ``````````````````````````````````````````````````````
        /// </remarks>
        /// <response code="200">
        ///     **Data** - The beer has been create.
        ///     **Unexcepted** - An unexecepted error occurs.
        /// </response>
        /// <param name="request">The model that allow to create beer</param>
        /// <returns></returns>
        [HttpPost("Remove")]
        public  Task<BaseHttpResponse<RemoveBeerResponse>> DeleteAsync(RemoveBeerRequest request)
            => ExecuteBlAsync(() => _bl.RemoveBeerAsync(request));
        
        
        
        
        /// <summary>
        ///     List all the beers by brewery and the wholesalers who sell it
        /// </summary>
        /// <remarks>
        ///   ## Header
        ///   ``````````````````````````````````````````````````````
        ///     Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MjYxNTQwNTgsImlzcyI6IkJyZXdlcnlsb2NhbFNlY3JldEtleSIsImF1ZCI6ImJyZXdlcnkubG9jYWwuY29tIn0.0JSBKqXH1sB6voiZa6x2l1x6YI-Q1kA0xQG9lkH3vHs
        ///   ``````````````````````````````````````````````````````
        /// 
        ///     ## Example Request ##
        ///     ``````````````````````````````````````````````````````
        ///     POST /api/Beer/Search
        ///     {
        ///         "Breweries": ["08d9c2d4-bd3b-4129-8153-4924870c9cb7", "Leffe Blonde"],
        ///         "Wholesalers": ["08d9c2d4-bd3b-4129-8153-4924870c9cb2", "GeneDrinks"],
        ///         "Take": 30,
        ///         "Skip": 0
        ///     }
        ///     ``````````````````````````````````````````````````````
        ///     *Create beer.*
        ///
        ///
        ///     ## Example Response ##
        ///     ``````````````````````````````````````````````````````
        ///     When Reason exist, data is null. When Data exist, reason must be null
        ///      {
        ///        Reason: "Some error if error",
        ///        ResultStatus: "SUCCESS",
        ///        Data: {
        ///         Count: 1,
        ///         Results: [
        ///             {
        ///                "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                "Name": "Leffe Blonde",
        ///                "Price": 2.20,
        ///                "Description": "A description",
        ///                "Degree": "6.6%",
        ///                "Owner": { Name: "Abbaye de Leffe", Id: "3fa85f64-5717-4562-b3fc-2c963f66afa1"}
        ///             }
        ///          ]
        ///        },
        ///     }
        ///     
        ///     ``````````````````````````````````````````````````````
        /// </remarks>
        /// <response code="200">
        ///     **Data** - The beer has been create.
        ///     **Unexcepted** - An unexecepted error occurs.
        /// </response>
        /// <param name="request">The model that allow to create beer</param>
        /// <returns></returns>
        [HttpPost("Search")]

        public  Task<BaseHttpResponse<ListResult<SearchBeerByWholesalerAndBreweryResponse>>> GetBeerByWholesalersOrBreweriesAsync([FromBody] SearchBeerByWholesalerAndBreweryRequest request)
            => ExecuteBlAsync(() => _bl.GetBeerByWholesalersOrBreweries(request));

        
        
        
           /// <summary>
        ///     List all the beers 
        /// </summary>
        /// <remarks>
        ///   ## Header
        ///   ``````````````````````````````````````````````````````
        ///     Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MjYxNTQwNTgsImlzcyI6IkJyZXdlcnlsb2NhbFNlY3JldEtleSIsImF1ZCI6ImJyZXdlcnkubG9jYWwuY29tIn0.0JSBKqXH1sB6voiZa6x2l1x6YI-Q1kA0xQG9lkH3vHs
        ///   ``````````````````````````````````````````````````````
        /// 
        ///     ## Example Request ##
        ///     ``````````````````````````````````````````````````````
        ///     GET /api/Beer/0/30
        ///     ``````````````````````````````````````````````````````
        ///     *get all beer.*
        ///
        ///     ## Example Response ##
        ///     ``````````````````````````````````````````````````````
        ///     When Reason exist, data is null. When Data exist, reason must be null
        ///      {
        ///        Reason: "Some error if error",
        ///        ResultStatus: "SUCCESS",
        ///        Data: {
        ///         Count: 1,
        ///         Results: [
        ///             {
        ///                "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///                "Name": "Leffe Blonde",
        ///                "Price": 2.20,
        ///                "Description": "A description",
        ///                "Degree": "6.6%",
        ///                "Owner": { Name: "Abbaye de Leffe", Id: "3fa85f64-5717-4562-b3fc-2c963f66afa1"}
        ///             }
        ///          ]
        ///        },
        ///     }
        ///     
        ///     ``````````````````````````````````````````````````````
        /// </remarks>
        /// <response code="200">
        ///     **Data** - The beers has be found.
        ///     **Unexcepted** - An unexecepted error occurs.
        /// </response>
        /// <param name="take">The count to take</param>
        /// <param name="skip">The count to skip</param>
        /// <returns></returns>
        [HttpGet("{skip}/{take}")]
        public  Task<BaseHttpResponse<ListResult<SearchBeerByWholesalerAndBreweryResponse>>> GetAllAsync(int skip, int take)
            => ExecuteBlAsync(() => _bl.GetAllAsync(skip,  take));

        #endregion

    }