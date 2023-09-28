using Brewery.Contract.Contracts.Responses.Companies;
using Brewery.DAL.Repositories.Wholesalers;
using Elia.Core.Attributes;
using Elia.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace Brewery.BL.Business.Wholesalers;

    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management 
    ///         of Brewery.
    ///     </para>
    /// </summary>
    [Injectable()]
    public class WholesalerBl
    {
        #region Properties (Private)

        /// <summary>
        ///     <para>
        ///         This property represents the repository
        ///     </para>
        /// </summary>
        private readonly WholesalerRepository _repository;
        
        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"> beer repository</param>
        public WholesalerBl(WholesalerRepository repository)
        {
            _repository = repository;
        }

        #endregion
        
        
        #region Public methods

      
        
        /// <summary>
        /// Create a new beer
        /// </summary>
        /// <param name="search"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public async  Task<BaseResult<ListResult<GetCompanyResponse>>> GetWholesalerAsync(string search, int take, int skip)
        {
            var entityReponse = await _repository.SearchAllByAsync(b => string.IsNullOrEmpty(search) || !string.IsNullOrEmpty(search) &&
                                                                        EF.Functions.Like(b.Name, "%" + search + "%"), 
                o => o.OrderBy(b => b.Name), null,  skip, take);

            if (entityReponse.IsSuccess)
            {
                var response = entityReponse.Data.Results.Select(b =>
                    new GetCompanyResponse()
                    {
                        Name = b.Name,
                        Id = b.Id,
                    }).ToList();
                
                return new BaseResult<ListResult<GetCompanyResponse>>(new ListResult<GetCompanyResponse>(response, entityReponse.Data.Count));
            }

            return new BaseResult<ListResult<GetCompanyResponse>>(entityReponse.Status, entityReponse.Exception);
        }
        
        #endregion

    }