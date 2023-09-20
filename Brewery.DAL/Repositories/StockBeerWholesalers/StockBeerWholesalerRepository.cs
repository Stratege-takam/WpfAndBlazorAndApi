using Brewery.BO.Entities;
using Brewery.Context;
using Elia.Core.Attributes;
using Elia.Core.Repositories;
using Elia.Core.Utils;

namespace Brewery.DAL.Repositories.StockBeerWholesalers;


    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management 
    ///         of stock beer wholesalers.
    ///     </para>
    /// </summary>
    [Injectable()]
    public class StockBeerWholesalerRepository : BaseRepository<BreweryContext, StockBeerWholesalerEntity>
    {
        #region Properties (Private)

        /// <summary>
        ///     <para>
        ///         This property represents the context of the application and will be injected in the controller of this class
        ///     </para>
        /// </summary>
        private readonly BreweryContext _context;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public StockBeerWholesalerRepository(BreweryContext context) : base(context)
        {
            _context = context;
        }

        #endregion
        
        
          
        #region Public Method

        /// <summary>
        /// Decrease quantity of beer in stock
        /// </summary>
        /// <param name="beerId">Beer id</param>
        /// <param name="count">count beer to decrease</param>
        /// <param name="wholesalerId">wholesaler id of beer </param>
        /// <returns></returns>
        public  async Task<BaseResult<StockBeerWholesalerEntity>> DecreaseQuantityOfBeer( Guid beerId, int count, Guid wholesalerId)
        {
            var entityResponse = await base.SearchOneByByAsync(b => b.BeerId.Equals(beerId) && b.WholesalerId.Equals(wholesalerId));

            if (entityResponse.IsSuccess)
            {
                
                if (count > entityResponse.Data.Stock) // return error
                {
                    return new BaseResult<StockBeerWholesalerEntity>(BaseResultStatus.Failure,
                        new Exception("The quantity cannot be greater than the stock"));
                }
                else if(count == entityResponse.Data.Stock) // we remove stock
                {
                    entityResponse = await base.DeletePermanentlyAsync(entityResponse.Data.Id);
                }
                else // Update quanity
                {
                    entityResponse.Data.Stock -= count;
                    entityResponse = await base.UpdateAsync(entityResponse.Data);
                }
            }

            return entityResponse;
        }
        
        
        
        /// <summary>
        /// Check if beer is in stock
        /// </summary>
        /// <param name="beerId">Beer id or name</param>
        /// <param name="count">desired number of beers</param>
        /// <param name="wholesalerId">seller id</param>
        /// <returns></returns>
        public Task<BaseResult<bool>> BeerIsInStock(string beerId, int count, Guid wholesalerId)
        {
            return AnyAsync(b => (b.BeerId.ToString().Equals(beerId) || b.Beer.Name.Equals(beerId))
                                 && b.WholesalerId == wholesalerId && b.Stock >= count);
        }
        
        
        /// <summary>
        /// check if the wholesaler sells this brand of beer
        /// </summary>
        /// <param name="beerId"></param>
        /// <param name="wholesalerId"></param>
        /// <returns></returns>
        public Task<BaseResult<bool>> HaveBeer(string beerId, Guid wholesalerId)
        {
            return AnyAsync(b => (b.BeerId.ToString().Equals(beerId) || b.Beer.Name.Equals(beerId))
                                 && b.WholesalerId == wholesalerId);
        }

        #endregion
     
     
    }