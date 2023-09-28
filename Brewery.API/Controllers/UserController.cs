using Brewery.BL.Business.Users;
using Brewery.Contract.Contracts.Requests.Users;
using Brewery.Contract.Contracts.Responses.Users;
using Elia.Core.Utils;
using Microsoft.AspNetCore.Mvc;
namespace Brewery.API.Controllers;

     /// <summary>
    /// User Controller class
    /// </summary>
    [Route("api/[controller]"), 
     ApiController]
    public class UserController : BaseController<UserController>
    {
        #region Properties (Private)

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Represents the business logic of the beer that will be injected in this controller
        /// </summary>
        private readonly UserBl _bl;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="bl">Represents the injection of the CommentLikes business logic</param>
        public UserController(ILogger<UserController> logger, UserBl bl) : base(logger)
        {
            _logger = logger;
            _bl = bl;
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
        [HttpPost("")]
        public  Task<BaseHttpResponse<CreateUserResponse>> CreateuserAsync([FromBody] CreateUserRequest request)
            => ExecuteBlAsync(() => _bl.CreateUserAsync(request));
        
        
        
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
        [HttpPost("Login")]
        public  Task<BaseHttpResponse<CreateUserResponse>> LoginAsync([FromBody] LoginRequest request)
            => ExecuteBlAsync(() => _bl.LoginAsync(request));
        
    
        #endregion
    }