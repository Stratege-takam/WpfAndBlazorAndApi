using Brewery.BL.Contracts.Requests.Beers;
using Brewery.BL.Contracts.Responses.Beers;
using Brewery.BO.Entities;
using Brewery.DAL.Repositories.Beers;
using Brewery.DAL.Repositories.Breweries;
using Brewery.DAL.Repositories.OrderBeers;
using Brewery.DAL.Repositories.StockBeerWholesalers;
using Elia.Core.Attributes;
using Elia.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace Brewery.BL.Business.Beers;

    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management 
    ///         of Beer.
    ///     </para>
    /// </summary>
    [Injectable()]
    public class BeerBl
    {
        #region Properties (Private)

        /// <summary>
        ///     <para>
        ///         This property represents the repository
        ///     </para>
        /// </summary>
        private readonly BeerRepository _repository;
        
        /// <summary>
        /// Order beer repository
        /// </summary>
        private readonly OrderBeerRepository _orderBeerRepository;
        
        /// <summary>
        /// Brewery repository
        /// </summary>
        private readonly BreweryRepository _breweryRepository;
        
        
        /// <summary>
        /// Stock beer wholesaler repository
        /// </summary>
        private readonly StockBeerWholesalerRepository _stockBeerWholesalerRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"> beer repository</param>
        /// <param name="stockBeerWholesalerRepository">  Stock beer wholesaler repository </param>
        /// <param name="orderBeerRepository">Order beer repository</param>
        /// <param name="breweryRepository"></param>
        public BeerBl(BeerRepository repository, 
            StockBeerWholesalerRepository stockBeerWholesalerRepository,
            OrderBeerRepository orderBeerRepository, BreweryRepository breweryRepository)
        {
            _repository = repository;
            _stockBeerWholesalerRepository = stockBeerWholesalerRepository;
            _orderBeerRepository = orderBeerRepository;
            _breweryRepository = breweryRepository;
        }

        #endregion
        
        
        #region Public methods

      
        
        /// <summary>
        /// Create a new beer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<BaseResult<CreateBeerResponse>> CreateBeerAsync(CreateBeerRequest request)
        {

            if ( (await _breweryRepository.AnyAsync(b => b.Id == request.OwnerId)).IsNotSuccess)
            {
                return new BaseResult<CreateBeerResponse>(BaseResultStatus.BadParams,
                    new Exception("The identification of the introduced owner does not exist"));
            }
            
            if ( (await _repository.AnyAsync(b => b.Name.ToLower().Replace(" ", "") == request.Name.ToLower().Replace(" ", ""))).IsSuccess)
            {
                return new BaseResult<CreateBeerResponse>(BaseResultStatus.BadParams,
                    new Exception("A beer already exists under this name. Beer name must be unique"));
            }
            
            var entity = new BeerEntity()
            {
                Degree = request.Degree,
                Name = request.Name,
                Price = request.Price,
                OwnerId = request.OwnerId
            };
            var entityReponse = await _repository.CreateAsync(entity, true);

            if (entityReponse.IsSuccess)
            {
                var response = new CreateBeerResponse()
                {
                    Degree = entityReponse.Data.DegreePercent,
                    Name = entityReponse.Data.Name,
                    Price = entityReponse.Data.Price,
                    Id = entityReponse.Data.Id,
                };
                
                return new BaseResult<CreateBeerResponse>(response);
            }

            return new BaseResult<CreateBeerResponse>(entityReponse.Status, entityReponse.Exception);
        }
        
        
        
        /// <summary>
        /// Remove beer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<BaseResult<RemoveBeerResponse>> RemoveBeerAsync(RemoveBeerRequest request)
        {
            Guid id;
            var isId = Guid.TryParse(request.Name, out id);
            
            var entityReponse =
                await _repository.SearchOneByByAsync(b => isId && b.Id.Equals(id) || b.Name.Equals(request.Name));
                
            // Check if a beer exist   
            if (entityReponse.IsNotSuccess)
            {
                return new BaseResult<RemoveBeerResponse>(entityReponse.Status, entityReponse.Exception);
            }
            
            // check if beer is used by another table, update deleteDate else delete permanently
            if ((await _orderBeerRepository.AnyAsync(o => o.BeerId == entityReponse.Data.Id)).IsSuccess ||
                (await _stockBeerWholesalerRepository.AnyAsync(b => b.BeerId.Equals(entityReponse.Data.Id))).IsSuccess)
            {
                entityReponse = await _repository.DeleteAsync(entityReponse.Data);  //Temporary deletion
            }
            else
            {
                entityReponse = await _repository.DeletePermanentlyAsync(entityReponse.Data.Id);  //permanently deletion
            }

            if (entityReponse.IsSuccess)
            {
                var response = new RemoveBeerResponse()
                {
                    Id = entityReponse.Data.Id,
                };
                
                return new BaseResult<RemoveBeerResponse>(response);
            }

            return new BaseResult<RemoveBeerResponse>(entityReponse.Status, entityReponse.Exception);
        }
        
        
        
        
        /// <summary>
        /// Get all beer by wholesaler or breweries
        /// </summary>
        /// <param name="request">Payload</param>
        /// <returns></returns>
        public async Task<BaseResult<ListResult<SearchBeerByWholesalerAndBreweryResponse>>> GetBeerByWholesalersOrBreweries(SearchBeerByWholesalerAndBreweryRequest request)
        {
            var response = await _repository.GetBeerByWholesalersOrBreweries(request.Breweries, request.Wholesalers, request.Skip,
                request.Take);

            if (response.IsSuccess)
            {
                var result = response.Data.Results.Select(b => new SearchBeerByWholesalerAndBreweryResponse()
                {
                    Degree = b.DegreePercent,
                    Name = b.Name,
                    Price = b.Price,
                    Id = b.Id,
                    Owner = new BreweryCreateBeerResponse(b.Owner.Name, b.Owner.Id) 
                });
                
                return new BaseResult<ListResult<SearchBeerByWholesalerAndBreweryResponse>>(new ListResult<SearchBeerByWholesalerAndBreweryResponse>(result, response.Data.Count));
            }

            return new BaseResult<ListResult<SearchBeerByWholesalerAndBreweryResponse>>(response.Status, response.Exception);
        }
        
        
        /// <summary>
        /// Get all beers
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async Task<BaseResult<ListResult<SearchBeerByWholesalerAndBreweryResponse>>> GetAllAsync(int skip, int take)
        {
            var response = await _repository.GetAllAsync( skip,
                take, o=> o.OrderBy(b => b.Name), i=> i.Include(b => b.Owner));
            
            if (response.IsSuccess)
            {
                var result = response.Data.Results.Select(b => new SearchBeerByWholesalerAndBreweryResponse()
                {
                    Degree = b.DegreePercent,
                    Name = b.Name,
                    Price = b.Price,
                    Id = b.Id,
                    Owner = new BreweryCreateBeerResponse(b.Owner.Name, b.Owner.Id) 
                });
                
                return new BaseResult<ListResult<SearchBeerByWholesalerAndBreweryResponse>>(new ListResult<SearchBeerByWholesalerAndBreweryResponse>(result, response.Data.Count));
            }

            return new BaseResult<ListResult<SearchBeerByWholesalerAndBreweryResponse>>(response.Status, response.Exception);
        }

       
        #endregion

      
    }