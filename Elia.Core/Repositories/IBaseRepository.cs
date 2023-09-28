using Elia.Core.Utils;
namespace Elia.Core.Repositories;
using System.Linq.Expressions;



  /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity: class
    {
        #region Methodes

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="validateEntity"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        Task<BaseResult<TEntity>> CreateAsync(TEntity obj, bool validateEntity = false,
            params string[] excluPropertiesInResult);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        /// <param name="validateEntity"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        Task<BaseResult<IList<TEntity>>> CreateRangeAsync(IList<TEntity> objs, bool validateEntity = false,
            params string[] excluPropertiesInResult);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        Task<BaseResult<IList<TEntity>>> DeletePermanenlyRangeAsync(IList<TEntity> objs);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        Task<BaseResult<IList<TEntity>>> DeleteRangeAsync(IList<TEntity> objs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        /// <param name="validateEntity"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        Task<BaseResult<IList<TEntity>>> UpdateRangeAsync(IList<TEntity> objs,
            bool validateEntity = false, params string[] excluPropertiesInResult);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="validateEntity"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        Task<BaseResult<TEntity>> UpdateAsync(TEntity obj,
            bool validateEntity = false, params string[] excluPropertiesInResult);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="includeDeletedEntry"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        Task<BaseResult<ListResult<TEntity>>> SearchAllByAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            int? skip = null, int? take = null, bool includeDeletedEntry = false,
            params string[] excluPropertiesInResult);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <param name="includeDeletedEntry"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        Task<BaseResult<TEntity>> SearchOneByByAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            bool includeDeletedEntry = false, params string[] excluPropertiesInResult);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDeletedEntry"></param>
        /// <returns></returns>
        Task<BaseResult<bool>> AnyAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            bool includeDeletedEntry = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDeletedEntry"></param>
        /// <returns></returns>
        Task<BaseResult<int>> CountAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            bool includeDeletedEntry = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<TEntity>> DeletePermanentlyAsync(Guid? id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<BaseResult<TEntity>> DeleteAsync(TEntity obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDeletedEntry"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        Task<BaseResult<TEntity>> GetAsync(Guid? id, bool includeDeletedEntry = false,
            params string[] excluPropertiesInResult);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="includeDeletedEntry"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        Task<BaseResult<ListResult<TEntity>>> GetAllAsync(int? skip = null, int? take = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null, bool includeDeletedEntry = false,
            params string[] excluPropertiesInResult);

        #endregion
    }
