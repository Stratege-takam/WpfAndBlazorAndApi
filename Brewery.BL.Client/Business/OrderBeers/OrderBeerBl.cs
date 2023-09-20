using Brewery.BL.Client.Contracts.Inputs.Orders;
using Brewery.BL.Client.Contracts.Outputs.Orders;
using Elia.Core.Attributes;
using Elia.Core.Enums;
using Elia.Core.Services.ServerRest;
using Elia.Core.Utils;
using Microsoft.Extensions.Options;

namespace Brewery.BL.Client.Business.OrderBeers;


    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management 
    ///         of order Beer.
    ///     </para>
    /// </summary>
    [Injectable()]
    public class OrderBeerBl : FormatResult
    {
        #region Constructor

       
        public OrderBeerBl(ServerRestService http, IOptions<AppSettings.Server> appSettingsClientSection): base(http, appSettingsClientSection.Value)
        {
        }

        #endregion
        
        
         #region Public methods

        
        /// <summary>
        ///     Request a quote from a wholesaler
        /// </summary>
        /// <remarks>
        ///   ## Header
        ///   ``````````````````````````````````````````````````````
        ///     Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MjYxNTQwNTgsImlzcyI6IkJyZXdlcnlsb2NhbFNlY3JldEtleSIsImF1ZCI6ImJyZXdlcnkubG9jYWwuY29tIn0.0JSBKqXH1sB6voiZa6x2l1x6YI-Q1kA0xQG9lkH3vHs
        ///   ``````````````````````````````````````````````````````
        /// 
        ///     ## Example Request ##
        ///     ``````````````````````````````````````````````````````
        ///     POST /api/OrderBeer
        ///     {
        ///         "WholesalerId": "08d9c2d4-bd3b-4129-8153-4924870c9cb1",
        ///         "Beers": [
        ///             {"Name": "Leffe Blonde", "Count": 2},
        ///             {"Name": "Leffe black", "Count": 25}
        ///         ]
        ///         
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
        ///         "Discount": 0.2,
        ///         "Total": 132.40,
        ///         "DiscountAmount": 26.48,
        ///         "TotalToPay": 105.92,
        ///         "Beers": [
        ///             {"Name": "Leffe Blonde", "Count": 2, "Price": 2.20, "Degree": "6.6%", "Total": 4.40},
        ///             {"Name": "Leffe black", "Count": 25, "Price": 5.12, "Degree": "16.23%", "Total": 128.00}
        ///         ]
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
        public  Task<BaseHttpResponse<EstimateOrderOutput>> GetEstimation(EstimationOrderInput request)
            => ExecuteAsync(() => Http.RunAsync<BaseHttpResponse<EstimateOrderOutput>>($"{BaseUrl}/OrderBeer/Estimations", Verb.POST, request));
        
        
        /// <summary>
        ///     Add the sale of an existing beer, to an existing wholesaler
        /// </summary>
        /// <remarks>
        ///   ## Header
        ///   ``````````````````````````````````````````````````````
        ///     Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MjYxNTQwNTgsImlzcyI6IkJyZXdlcnlsb2NhbFNlY3JldEtleSIsImF1ZCI6ImJyZXdlcnkubG9jYWwuY29tIn0.0JSBKqXH1sB6voiZa6x2l1x6YI-Q1kA0xQG9lkH3vHs
        ///   ``````````````````````````````````````````````````````
        /// 
        ///     ## Example Request ##
        ///     ``````````````````````````````````````````````````````
        ///     POST /api/OrderBeer
        ///     {
        ///         "ClientId": "08d9c1d4-bd3b-4129-8153-4924870c9cb7",
        ///         "WholesalerId": "08d9c2d4-bd3b-4129-8153-4924870c9cb1",
        ///         "Beers": [
        ///             {"Name": "Leffe Blonde", "Count": 2},
        ///             {"Name": "Leffe black", "Count": 25}
        ///         ]
        ///         
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
        ///         "Discount": 0.2,
        ///         "Total": 132.40,
        ///         "DiscountAmount": 26.48,
        ///         "TotalToPay": 105.92,
        ///         "Beers": [
        ///             {"Name": "Leffe Blonde", "Count": 2, "Price": 2.20, "Degree": "6.6%", "Total": 4.40},
        ///             {"Name": "Leffe black", "Count": 25, "Price": 5.12, "Degree": "16.23%", "Total": 128.00}
        ///         ]
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
        /// <param name="request">The model that allow to create order beer</param>
        /// <returns></returns>
        public  Task<BaseHttpResponse<EstimateOrderOutput>> PostAsync(CreateOrderInput request)
            => ExecuteAsync(() => Http.RunAsync<BaseHttpResponse<EstimateOrderOutput>>($"{BaseUrl}/OrderBeer", Verb.POST, request));

        #endregion
    }