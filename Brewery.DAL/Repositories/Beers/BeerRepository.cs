using Brewery.BO.Entities;
using Brewery.Context;
using Elia.Core.Attributes;
using Elia.Core.Repositories;
using Elia.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace Brewery.DAL.Repositories.Beers;



    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management 
    ///         of Beer.
    ///     </para>
    /// </summary>
    [Injectable()]
    public class BeerRepository : BaseRepository<BreweryContext, BeerEntity>
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
        public BeerRepository(BreweryContext context) : base(context)
        {
            _context = context;
        }

        #endregion


        #region Public methods

      
        /// <summary>
        /// Get all beer by wholesaler or breweries
        /// </summary>
        /// <param name="breweries">List id or name of breweries</param>
        /// <param name="wholesalers">Lis id or name of wholesaler</param>
        /// <param name="skip"> count skip</param>
        /// <param name="take">count take</param>
        /// <returns></returns>
        public  Task<BaseResult<ListResult<BeerEntity>>> GetBeerByWholesalersOrBreweries(IList<string> breweries, IList<string> wholesalers, 
            int? skip = null, int? take = null)
        {
            return base.SearchAllByAsync(b => 
                    breweries != null && (breweries.Any(c => c == b.Owner.Name) || breweries.Any(c => c == b.OwnerId.ToString())) ||
                    wholesalers != null && (b.StockBeerWholesalers.Any(s => wholesalers.Any(c => c == s.Wholesaler.Name))  || b.StockBeerWholesalers.Any(s => wholesalers.Any(c => c == s.WholesalerId.ToString())))
                , o => o.OrderBy(b => b.Name), i => i.Include(b=> b.Owner), skip, take);
        }
        
        
        
        /// <summary>
        /// Get all beer by name or id
        /// </summary>
        /// <param name="names">names or ids of beer</param>
        /// <returns></returns>
        public  Task<BaseResult<ListResult<BeerEntity>>> GetAllBeerIdByNameOrId(IList<string> names)
        {
          
            return SearchAllByAsync(b => names.Any(n => b.Name.Equals(n))  || names.Contains(b.Id.ToString()), null, null, 0,
                Int32.MaxValue);
        }

        #endregion
    }