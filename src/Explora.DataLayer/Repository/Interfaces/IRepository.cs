using Explora.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Explora.DataLayer.Repository.Interfaces
{
    public interface IRepository
    {
        #region Create

        TKey Create<TEntity, TKey>(TEntity entity) where TEntity : Entity<TKey>;

        IEnumerable<TKey> Create<TEntity, TKey>(IEnumerable<TEntity> entities) where TEntity : Entity<TKey>;

        #endregion

        #region Update

        bool Update<TEntity>(TEntity entity) where TEntity : class;

        #endregion

        #region Delete

        /// <summary>
        /// Removes an entity using the id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns>Entity removed</returns>
        TEntity Delete<TEntity,TKey>(TKey id) where TEntity : Entity<TKey>;

        bool Delete<TEntity>(TEntity entity) where TEntity : class;

        bool Delete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        #endregion

        #region Read

        IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool noTracking = false) where TEntity : class;

        IEnumerable<object> GetAll<TEntity>(
            Expression<Func<TEntity, object>> selector,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool noTracking = false) where TEntity : class;

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool noTracking = false) where TEntity : class;

        Task<IEnumerable<object>> GetAllAsync<TEntity>(
            Expression<Func<TEntity, object>> selector,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool noTracking = false) where TEntity : class;

        IEnumerable<TEntity> GetByFilter<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> selector = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool noTracking = false) where TEntity : class;

        Task<IEnumerable<TEntity>> GetByFilterAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool noTracking = false) where TEntity : class;

        Task<IEnumerable<object>> GetByFilterAsync<TEntity>(
            Expression<Func<TEntity, object>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool noTracking = false) where TEntity : class;

        TEntity GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool noTracking = false) where TEntity : class;

        object GetFirst<TEntity>(
            Expression<Func<TEntity, object>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool noTracking = false) where TEntity : class;

        Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool noTracking = false) where TEntity : class;

        Task<object> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, object>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool noTracking = false) where TEntity : class;

        TEntity GetEntityById<TEntity,TKey>(
            TKey id,
            bool noTracking = false) where TEntity : Entity<TKey>;

        object GetEntityById<TEntity,TKey>(
            TKey id,
            Expression<Func<TEntity, object>> selector,
            bool noTracking = false) where TEntity : Entity<TKey>;

        Task<TEntity> GetEntityByIdAsync<TEntity, TKey>(
            TKey id, 
            bool noTracking = false) where TEntity : Entity<TKey>;

        Task<object> GetEntityByIdAsync<TEntity,TKey>(
            TKey id,
            Expression<Func<TEntity, object>> selector,
            bool noTracking = false) where TEntity : Entity<TKey>;

        int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;

        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;

        #endregion
    }
}
