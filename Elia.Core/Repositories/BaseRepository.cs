using Elia.Core.BaseModel;
using Elia.Core.Utils;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Elia.Core.Extensions;

namespace Elia.Core.Repositories;


/// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public abstract  class BaseRepository<TContext, TEntity> : IBaseRepository<TEntity>
        where TEntity : Track
        where TContext: DbContext
    {

        #region Properties (Privates)
        /// <summary>
        /// 
        /// </summary>
        private readonly DbSet<TEntity> _dbset;
        
        /// <summary>
        /// 
        /// </summary>
        private readonly TContext _context;

        #endregion


        #region Constructor
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(TContext context)
        {
            _dbset = context.Set<TEntity>();
            _context = context;
        }

        #endregion

        #region Methodes (Public)
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDeletedEntry"></param>
        /// <returns></returns>
       public async Task<BaseResult<int>> CountAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            bool includeDeletedEntry = false)
        {
            try
            {
                var source = (IQueryable<TEntity>) _dbset;

                if (predicate != null)
                {
                    source = source.Where(predicate);
                }
                else
                    return new BaseResult<int>(BaseResultStatus.BadParams, new Exception("The predicate must be specify"));

                if (!includeDeletedEntry)
                {
                    source = source.Where(e => (e as Track).DeleteAt == null);
                }
                

                var query =   await source.CountAsync(); 
            
          
                return new BaseResult<int>(query);
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<int>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<int>(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDeletedEntry"></param>
        /// <returns></returns>
        public async Task<BaseResult<bool>> AnyAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            bool includeDeletedEntry = false)
        {
            try
            {
                var source = (IQueryable<TEntity>) _dbset;

                if (predicate != null)
                {
                    source = source.Where(predicate);
                }
                else
                    return new BaseResult<bool>(BaseResultStatus.BadParams, new Exception("The predicate must be specify"));

                if (!includeDeletedEntry)
                {
                    source = source.Where(e => e.DeleteAt == null);
                }
                

                var query =   await source.AnyAsync();

                if (query)
                {
                    return new BaseResult<bool>(query);
                }
                
                return new BaseResult<bool>(BaseResultStatus.NotFound, new Exception("Entry not found"));

               
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<bool>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<bool>(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="validateEntity"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        public virtual async Task<BaseResult<TEntity>> CreateAsync(TEntity obj, 
            bool validateEntity = false, params string[] excluPropertiesInResult)
        {
            try
            {
                obj.CreateAt = DateTime.Now;
                obj.UpdateAt = DateTime.Now;
                obj.DeleteAt = null;
                if (validateEntity)
                {
                    if (!obj.IsValid())
                        return new BaseResult<TEntity>(BaseResultStatus.BadParams).WithReason(obj.GetErrors());
                }
                _context.Add(obj);
                await _context.SaveChangesAsync();
                
                
                TrySetProperty(obj, null, excluPropertiesInResult);
               
                return new BaseResult<TEntity>(obj);
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<TEntity>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<TEntity>(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <param name="includeDeletedEntry"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        public virtual async Task<BaseResult<TEntity>> SearchOneByByAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            bool includeDeletedEntry = false,  params string[] excluPropertiesInResult)
        {
            try
            {
                var source = (IQueryable<TEntity>) _dbset;

                if (predicate != null)
                {
                    source = source.Where(predicate);
                }
                else
                    return new BaseResult<TEntity>(BaseResultStatus.BadParams, new Exception("The predicate must be specify"));

                if (!includeDeletedEntry)
                {
                    source = source.Where(e => e.DeleteAt == null);
                }
                
                if (include != null)
                {
                    source = include(source);
                }

                var query =   await source.SingleOrDefaultAsync();

                if (query == null)
                    return new BaseResult<TEntity>(BaseResultStatus.NotFound, new Exception("Item not found"));
               
                TrySetProperty(query, null, excluPropertiesInResult);
          
                return new BaseResult<TEntity>(query);
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<TEntity>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<TEntity>(exception);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<BaseResult<TEntity>> DeleteAsync(TEntity model)
        {
            try
            {
                if (model == null || !Guid.TryParse(model.Id.ToString(), out Guid re) )
                    return new BaseResult<TEntity>(BaseResultStatus.Unexpected, new Exception("The key param can be null"));
                
                var obj = await _dbset.FindAsync(model.Id);
                
                if (obj != null)
                {
                    obj.DeleteAt = DateTime.Now;
                    _context.Update(obj);
                    await _context.SaveChangesAsync();
                    _context.Entry(obj).State = EntityState.Detached;
                        
                    
                    return new BaseResult<TEntity>(obj);
                }
                return new BaseResult<TEntity>(BaseResultStatus.NotFound, new Exception("Item not found"));
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<TEntity>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<TEntity>(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<BaseResult<TEntity>> DeletePermanentlyAsync(Guid? id)
        {
            try
            {
                if (id == null)
                    return new BaseResult<TEntity>(BaseResultStatus.Unexpected, new Exception("The key param can be null"));
                
                TEntity obj = await _dbset.FindAsync(id);
                if (obj != null)
                {
                    _context.Remove(obj);
                    await _context.SaveChangesAsync();
                    
                    _context.Entry(obj).State = EntityState.Detached;
                        
                   
                    return new BaseResult<TEntity>(obj);
                }
                return new BaseResult<TEntity>(BaseResultStatus.NotFound, new Exception("Item not found"));
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<TEntity>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<TEntity>(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDeletedEntry"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        public virtual async Task<BaseResult<TEntity>> 
            GetAsync(Guid? id, bool includeDeletedEntry = false,
                params string[] excluPropertiesInResult)
        {
            try
            {
                var source = (IQueryable<TEntity>) _dbset;
                var query =   await _dbset.FindAsync(id);
                if (query != null)
                {
                    _context.Entry(query).State = EntityState.Detached;
                    
                }
                
                if (query == null || (!includeDeletedEntry && query.DeleteAt != null ))
                    return new BaseResult<TEntity>(BaseResultStatus.NotFound, new Exception("Item not found"));
                TrySetProperty(query, null, excluPropertiesInResult);
               
                return new BaseResult<TEntity>(query);
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<TEntity>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<TEntity>(exception);
            }
        }

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
        public virtual async Task<BaseResult<ListResult<TEntity>>> GetAllAsync(int? skip = null, int? take = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            bool includeDeletedEntry = false, 
            params string[] excluPropertiesInResult)
        {
            try
            {
                var source = (IQueryable<TEntity>) _dbset;
            
                if (!includeDeletedEntry)
                {
                    source = source.Where(e => e.DeleteAt == null);
                }
                
                var queryCount = source.CountAsync();
                var count = await queryCount;

                if (include != null)
                    source = include(source);
            
                if (orderBy != null)
                    source = orderBy(source);
            
                if (skip != null)          
                    source =  source.Skip(skip.Value);     
                           
                if (take != null)          
                    source = source.Take(take.Value);
            
                var queryAll = source.ToListAsync();  
                //Task.WaitAll(queryAll, queryCount);
                
                var list = await queryAll;
                list = list.Select(q =>
                {
                    TrySetProperty(q, null, excluPropertiesInResult);
                    return q;
                }).ToList();
                
                var result = new ListResult<TEntity>(list, count);
                return new BaseResult<ListResult<TEntity>>(result);
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<ListResult<TEntity>>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<ListResult<TEntity>>(exception);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public virtual async Task<BaseResult<IList<TEntity>>> DeleteRangeAsync(IList<TEntity> objs)
        {
            try
            {
                var i = 1;
                foreach (var model in objs)
                {
                    var obj  = await _dbset.FindAsync(model.Id);
                    obj.DeleteAt = DateTime.Now;
                    _context.Update(objs);
                    if (i% 15 == 0)
                        await _context.SaveChangesAsync();
                    i++;
                }
                await _context.SaveChangesAsync();
                
                return new BaseResult<IList<TEntity>>(objs);
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<IList<TEntity>>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<IList<TEntity>>(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public virtual async Task<BaseResult<IList<TEntity>>> DeletePermanenlyRangeAsync(IList<TEntity> objs)
        {
            try
            {
                _context.RemoveRange(objs);
                await _context.SaveChangesAsync();
                
                return new BaseResult<IList<TEntity>>(objs);
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<IList<TEntity>>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<IList<TEntity>>(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        /// <param name="validateEntity"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        public virtual async Task<BaseResult<IList<TEntity>>> CreateRangeAsync(IList<TEntity> objs,
            bool validateEntity = false, params string[] excluPropertiesInResult)
        {
            try
            {
                if (validateEntity)
                {
                    foreach (var model in objs)
                    {
                        model.UpdateAt = DateTime.Now;
                        model.CreateAt = DateTime.Now;
                        model.DeleteAt = null;
                        if (!model.IsValid())
                            return new BaseResult<IList<TEntity>>(BaseResultStatus.BadParams).WithReason(model.GetErrors());
                    }
                }

                var i = 1;
                foreach (var obj in objs)
                {
                    _context.Add(obj);
                    if (i% 15 == 0)
                        await _context.SaveChangesAsync();
                    i++;
                }
                await _context.SaveChangesAsync();
                
                objs = objs.Select(q =>
                {
                    TrySetProperty(q, null, excluPropertiesInResult);
                    return q;
                }).ToList();
                return new BaseResult<IList<TEntity>>(objs);
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<IList<TEntity>>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<IList<TEntity>>(exception);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        /// <param name="validateEntity"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        public virtual async Task<BaseResult<IList<TEntity>>> UpdateRangeAsync(IList<TEntity> objs,
           bool validateEntity = false, params string[] excluPropertiesInResult)
        {
            try
            {
                if (validateEntity)
                {
                    foreach (var model in objs)
                    {
                        model.UpdateAt = DateTime.Now;
                        model.DeleteAt = null;
                        if (!model.IsValid())
                            return new BaseResult<IList<TEntity>>(BaseResultStatus.BadParams).WithReason(model.GetErrors());
                    }
                }
                
                var i = 1;
                foreach (var obj in objs)
                {
                    _context.Update(obj);
                    if (i% 15 == 0)
                        await _context.SaveChangesAsync();
                    i++;
                }
                
                await _context.SaveChangesAsync();
                
                objs = objs.Select(q =>
                {
                    TrySetProperty(q, null, excluPropertiesInResult);
                    return q;
                }).ToList();
                return new BaseResult<IList<TEntity>>(objs);
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<IList<TEntity>>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<IList<TEntity>>(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="validateEntity"></param>
        /// <param name="excluPropertiesInResult"></param>
        /// <returns></returns>
        public virtual async Task<BaseResult<TEntity>> UpdateAsync(TEntity obj, 
            bool validateEntity = false, params string[] excluPropertiesInResult)
        {
            try
            {
                obj.UpdateAt = DateTime.Now;
                obj.DeleteAt = null;
                if (validateEntity)
                {
                    if (!obj.IsValid())
                        return new BaseResult<TEntity>(BaseResultStatus.BadParams).WithReason(obj.GetErrors());
                }
                _context.Update(obj);
                await _context.SaveChangesAsync();

                _context.Entry(obj).State = EntityState.Detached;
                
                TrySetProperty(obj, null, excluPropertiesInResult);
                return new BaseResult<TEntity>(obj);
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<TEntity>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<TEntity>(exception);
            }
        }

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
        public virtual async Task<BaseResult<ListResult<TEntity>>> SearchAllByAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            int? skip = null, int? take = null, bool includeDeletedEntry = false,
            params string[] excluPropertiesInResult)
        {
            try
            {
                var source = (IQueryable<TEntity>) _dbset;
            
                if (predicate != null)
                    source = source.Where(predicate);
            
                if (!includeDeletedEntry)
                {
                    source = source.Where(e => e.DeleteAt == null);
                }
                
                var queryCount = await source.CountAsync();

                if (queryCount == 0)
                    return new BaseResult<ListResult<TEntity>>(BaseResultStatus.NotFound,
                        new Exception("Item not found"));
            
                if (include != null)
                    source = include(source);
            
                if (orderBy != null)
                    source = orderBy(source);
            
                if (skip != null)          
                    source =  source.Skip(skip.Value);     
                           
                if (take != null)          
                    source = source.Take(take.Value);
            
                var queryAll = await source.ToListAsync();
                
                queryAll = queryAll.Select(q =>
                {
                    TrySetProperty(q, null, excluPropertiesInResult);
                    return q;
                }).ToList();
                var result = new ListResult<TEntity>(queryAll, queryCount);
                
                return new BaseResult<ListResult<TEntity>>(result);
            }
            catch (InvalidOperationException exception)
            {
                return new BaseResult<ListResult<TEntity>>(BaseResultStatus.Failure, exception);
            }
            catch (Exception exception)
            {
                return new BaseResult<ListResult<TEntity>>(exception);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public bool TrySetProperty(object obj, object value = null , params string[] properties)
        {
            try
            {
                bool? setAllProperty = null;
                foreach (var property in properties)
                {
                    var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
                    if(prop != null && prop.CanWrite) {
                        prop.SetValue(obj, value, null);
                        if (setAllProperty != false)
                        {
                            setAllProperty =  true;
                        }
                    }
                    else
                    {
                        setAllProperty = false;
                    }
                }
                
                return setAllProperty != null && setAllProperty.Value;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }
        #endregion
    }