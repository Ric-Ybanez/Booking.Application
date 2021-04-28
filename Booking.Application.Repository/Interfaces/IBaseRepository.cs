using Booking.Application.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Repository.Interfaces
{
    public interface IBaseRepository
    {
        /// <summary>
        /// gets single entity. DbEntity and DTO must be defined in mapping
        /// </summary>
        /// <typeparam name="PEntity"></typeparam>
        Task<PEntity> GetOneAsync<TEntity, PEntity>(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null, bool asNoTracking = false) where TEntity : class;

        /// <summary>
        /// gets enumerable of entity. order by must be supplied in order to use count/offset
        /// DbEntity and DTO must be defined in mapping
        /// </summary>
        /// <typeparam name="PEntity"></typeparam>
        Task<IEnumerable<PEntity>> GetAsync<TEntity, PEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTracking = true)
            where TEntity : class;

        Task<PEntity> GetFirstAsync<TEntity, PEntity>(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null, bool asNoTracking = true)
            where TEntity : class;

        Task<IEnumerable<PEntity>> GetAllAsync<TEntity, PEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTracking = true)
            where TEntity : class;

        /// <summary>
        /// attaches new entity to be inserted. SaveAsync() must be called to persist data.
        /// DbEntity and DTO must be defined in mapping
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="PEntity"></typeparam>
        void Insert<TEntity>(TEntity entity, int user  = -1) where TEntity : class, IEntity;

        /// <summary>
        /// updates entity. SaveAsync() must be called to persist data.
        /// DbEntity and DTO must be defined in mapping
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="PEntity"></typeparam>
        Task<TEntity> Update<TEntity, PEntity>(Expression<Func<TEntity, bool>> predicate, PEntity entity, int user = -1) where TEntity : class, IEntity;

        /// <summary>
        /// persists data to data repository. auditUser is the user who made the change
        /// </summary>
        /// <param name="auditUser"></param>
        /// <returns></returns>
        Task SaveAsync(int user = -1);
    }
}
