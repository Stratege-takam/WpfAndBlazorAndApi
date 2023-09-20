using Brewery.BO.Entities;
using Brewery.Context;
using Elia.Core.Attributes;
using Elia.Core.Repositories;

namespace Brewery.DAL.Repositories.Wholesalers;


    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management 
    ///         of stock beer wholesalers.
    ///     </para>
    /// </summary>
    [Injectable()]
    public class WholesalerRepository : BaseRepository<BreweryContext, WholesalerEntity>
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
        public WholesalerRepository(BreweryContext context) : base(context)
        {
            _context = context;
        }

        #endregion
     
    }