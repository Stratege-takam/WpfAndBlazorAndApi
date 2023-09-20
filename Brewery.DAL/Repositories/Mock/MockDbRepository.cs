using Brewery.Context;
using Brewery.Context.Seeds;
using Elia.Core.Attributes;
using Elia.Core.Utils;

namespace Brewery.DAL.Repositories.Mock;

    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management 
    ///         of Beer.
    ///     </para>
    /// </summary>
    [Injectable()]
    public class MockDbRepository
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
        public MockDbRepository(BreweryContext context) 
        {
            _context = context;
        }

        #endregion


        #region Public methods

      
        /// <summary>
        /// Clean all tables
        /// </summary>
        /// <returns></returns>
        public  Task<BaseResult<bool>> CleanTables()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Delete( "StockBeerWholesalers");
                    _context.Delete("OrderBeers");
                    _context.Delete("Orders");
                    _context.Delete("Beers");
                    _context.Delete("Breweries");
                    _context.Delete("Clients");
                    _context.Delete("Wholesalers");
                    _context.Delete("Companies");
                    _context.Delete("Users");
                    _context.Delete("Medias");
                    _context.Delete("Tracks");
                    _context.SaveChanges();
                    transaction.Commit();

                    return Task.Run(() => new BaseResult<bool>(true));
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    
                    return Task.Run(() => new BaseResult<bool>(e));
                }
            }
            
        }


        /// <summary>
        /// Run all seed 
        /// </summary>
        /// <param name="token">the token for default user</param>
        /// <returns></returns>
        public Task<BaseResult<bool>> RunSeed(string token)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    ClientSeed.Run(_context);
                    WholesalerSeed.Run(_context);
                    BrewerySeed.Run(_context);
                    BeerSeed.Run(_context);
                    OrderSeed.Run(_context);
                    OrderBeerSeed.Run(_context);
                    StockBeerWholesalerSeed.Run(_context);
                    UserSeed.Run(_context,token);
                    transaction.Commit();

                    return Task.Run(() => new BaseResult<bool>(true));
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    
                    return Task.Run(() => new BaseResult<bool>(e));
                }
            }
            
        }

        
        
        #endregion
    }