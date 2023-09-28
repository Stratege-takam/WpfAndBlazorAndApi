using Brewery.BO.Entities;
using Brewery.Context;
using Brewery.Contract.Contracts.Requests.Orders;
using Brewery.Contract.Contracts.Responses.Orders;
using Brewery.DAL.Repositories.Beers;
using Brewery.DAL.Repositories.Clients;
using Brewery.DAL.Repositories.OrderBeers;
using Brewery.DAL.Repositories.Orders;
using Brewery.DAL.Repositories.StockBeerWholesalers;
using Brewery.DAL.Repositories.Wholesalers;
using Elia.Core.Attributes;
using Elia.Core.Utils;

namespace Brewery.BL.Business.OrderBeers;


    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management 
    ///         of order Beer.
    ///     </para>
    /// </summary>
    [Injectable()]
    public class OrderBeerBl 
    {
        #region Properties (Private)

        /// <summary>
        ///     <para>
        ///         This property represents the repository
        ///     </para>
        /// </summary>
        private readonly OrderBeerRepository _repository;
        
        /// <summary>
        /// Repository of wholesaler
        /// </summary>
        private readonly WholesalerRepository _wholesalerRepository;
        
        /// <summary>
        /// Repository of beer
        /// </summary>
        private readonly BeerRepository _beerRepository;
        
        
        /// <summary>
        /// Repository order
        /// </summary>
        private readonly OrderRepository _orderRepository;
        
        /// <summary>
        /// Stock beer wholesaler repository
        /// </summary>
        private readonly StockBeerWholesalerRepository _stockBeerWholesalerRepository;
        
        /// <summary>
        /// Client repository
        /// </summary>
        private readonly ClientRepository _clientRepository;

        /// <summary>
        /// The context
        /// </summary>
        private readonly BreweryContext _context;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository">The repository</param>
        /// <param name="context">The context</param>
        /// <param name="wholesalerRepository">The wholesaler repository</param>
        /// <param name="stockBeerWholesalerRepository">Stock beer wholesaler repository</param>
        /// <param name="clientRepository">Client repository</param>
        /// <param name="beerRepository">Repository of beer</param>
        /// <param name="orderRepository">Repository order</param>
        public OrderBeerBl(OrderBeerRepository repository, BreweryContext context, 
            WholesalerRepository wholesalerRepository,
            StockBeerWholesalerRepository stockBeerWholesalerRepository, ClientRepository clientRepository,
            BeerRepository beerRepository, OrderRepository orderRepository)
        {
            _repository = repository;
            _context = context;
            _wholesalerRepository = wholesalerRepository;
            _stockBeerWholesalerRepository = stockBeerWholesalerRepository;
            _clientRepository = clientRepository;
            _beerRepository = beerRepository;
            _orderRepository = orderRepository;
        }

        #endregion


        #region Public methods

        /// <summary>
        /// Get estimation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseResult<EstimateOrderResponse>> GetEstimation(EstimationOrderRequest request)
        {
            
            var constraints = await CheckOrderRespectsConstraints(request);

            if (constraints.IsNotSuccess)
            {
                return constraints;
            }
            
            var beersResponse = await _beerRepository.GetAllBeerIdByNameOrId(request.Beers.Select(b => b.Name).ToList());

            if (beersResponse.IsSuccess)
            {
                var response = beersResponse.Data.Results.Select(b => new EstimateOrderItemResponse(b.Name,null,
                    request.Beers.FirstOrDefault(c => c.Name == b.Name || b.Id.ToString().Equals(c.Name)).Count, b.Price)).ToList();

                return new BaseResult<EstimateOrderResponse>(new EstimateOrderResponse()
                {
                    Beers = response
                });
            }
            
            return new BaseResult<EstimateOrderResponse>(beersResponse.Status, beersResponse.Exception);
        }
        
        
        
         /// <summary>
        /// Method to create a new order 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<BaseResult<EstimateOrderResponse>> CreateAsync(CreateOrderRequest request)
        {

            var constraints = await CheckOrderRespectsConstraints(request);

            if (constraints.IsNotSuccess)
            {
                return constraints;
            }
            
            if ((await _clientRepository.AnyAsync(w => w.Id == request.ClientId)).IsNotSuccess)
            {
                return new BaseResult<EstimateOrderResponse>(BaseResultStatus.BadParams,
                    new Exception("The client must exist"));
            }
            
            var transaction = await _context.Database.BeginTransactionAsync();

            // create order
            var orderResponse = await _orderRepository.CreateAsync(new OrderEntity()
            {
                ClientId = request.ClientId,
                WholesalerId = request.WholesalerId,
                CommandNumber = Generator.GenerateDigit(),
                IsSold = true
            });

            if (orderResponse.IsNotSuccess)
            {
                await transaction.RollbackAsync();
                return new BaseResult<EstimateOrderResponse>(orderResponse.Status, orderResponse.Exception);
            }
            

            var beersResponse = await _beerRepository.GetAllBeerIdByNameOrId(request.Beers.Select(b => b.Name).ToList());

            if (beersResponse.IsSuccess)
            {

                var entities = beersResponse.Data.Results.Select( b => new OrderBeerEntity()
                {
                    Count = request.Beers.FirstOrDefault(c => c.Name == b.Name ||  b.Id.ToString().Equals(c.Name) ).Count,
                    BeerId = b.Id,
                    OrderId = orderResponse.Data.Id,
                }).ToList();
                
                // Create order beers
                var response = await _repository.CreateRangeAsync(entities);

                if (response.IsNotSuccess)
                {
                    await transaction.RollbackAsync();
                    return new BaseResult<EstimateOrderResponse>(response.Status, response.Exception);
                }
            
                //decrease the wholesaler's stock

                foreach (var beer in response.Data)
                {
                    var stockResponse = await _stockBeerWholesalerRepository.DecreaseQuantityOfBeer(beer.BeerId, beer.Count, request.WholesalerId);

                    if (stockResponse.IsNotSuccess)
                    {
                        await transaction.RollbackAsync();
                        return new BaseResult<EstimateOrderResponse>(stockResponse.Status, stockResponse.Exception);
                    }
                }


                try
                {
                    var result = response.Data.Select(b => new EstimateOrderItemResponse(b.Beer.Name, b.BeerId, b.Count, b.Beer.Price)).ToList();
                
                    await transaction.CommitAsync();
                    return new BaseResult<EstimateOrderResponse>(new EstimateOrderResponse()
                    {
                        Beers = result
                    });
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    return new BaseResult<EstimateOrderResponse>(BaseResultStatus.Failure, e);
                }
                
            }

            await transaction.RollbackAsync();
            return new BaseResult<EstimateOrderResponse>(BaseResultStatus.Failure, new Exception("Not found beer"));
        }
        
        #endregion
        
        
        
        #region Private method

        private async Task<BaseResult<EstimateOrderResponse>> CheckOrderRespectsConstraints(EstimationOrderRequest request)
        {
            
            if (request.Beers == null || !request.Beers.Any())
            {
                return new BaseResult<EstimateOrderResponse>(BaseResultStatus.BadParams,
                    new Exception("Order cannot be empty"));
            }
            
            
            if ((await _wholesalerRepository.AnyAsync(w => w.Id == request.WholesalerId)).IsNotSuccess)
            {
                return new BaseResult<EstimateOrderResponse>(BaseResultStatus.BadParams,
                    new Exception("The wholesaler must exist"));
            }
            
            if (request.Beers.Any(b => request.Beers.Count(be => be.Name == b.Name) > 1))
            {
                return new BaseResult<EstimateOrderResponse>(BaseResultStatus.BadParams,
                    new Exception("There cannot be a duplicate in the order"));
            }

            foreach (var beer in request.Beers)
            {
                if ((await _stockBeerWholesalerRepository.HaveBeer(beer.Name, request.WholesalerId)).IsNotSuccess)
                {
                    return new BaseResult<EstimateOrderResponse>(BaseResultStatus.BadParams,
                        new Exception($"The beer ({beer.Name}) must be sold by the wholesaler"));
                }
                
                if ((await _stockBeerWholesalerRepository.BeerIsInStock(beer.Name, beer.Count, request.WholesalerId)).IsNotSuccess)
                {
                    return new BaseResult<EstimateOrderResponse>(BaseResultStatus.BadParams,
                        new Exception($"The number of beers ({beer.Name}) ordered must not exceed the wholesaler's stock"));
                }
            }

            // scenario success
            return new BaseResult<EstimateOrderResponse>(new EstimateOrderResponse());

        }
        #endregion
    }