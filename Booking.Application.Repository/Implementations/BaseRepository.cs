using AutoMapper;
using AutoMapper.QueryableExtensions;
using Booking.Application.DAL;
using Booking.Application.DAL.Interfaces;
using Booking.Application.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Repository.Implementations
{
    public abstract class BaseRepository<TContext> : IBaseRepository where TContext : BookingAppContext
    {
        protected TContext _context;
        protected readonly IMapper _mapper;
        protected readonly ILogger _log;

        public BaseRepository(TContext context, IMapper mapper, ILogger log)
        {
            if (context == null) throw new ArgumentNullException("context");

            _context = context;
            _mapper = mapper;
            _log = log;
        }

        public virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTracking = true)
            where TEntity : class
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (asNoTracking)
                query = query.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public virtual async Task<PEntity> GetOneAsync<TEntity, PEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            bool asNoTracking = true)
            where TEntity : class
        {
            return await GetQueryable<TEntity>(filter, null, includeProperties, null, null, asNoTracking).ProjectTo<PEntity>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public virtual async Task<PEntity> GetFirstAsync<TEntity, PEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            bool asNoTracking = true)
            where TEntity : class
        {
            return await GetQueryable<TEntity>(filter, orderBy, includeProperties, null, null, asNoTracking).ProjectTo<PEntity>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<PEntity>> GetAsync<TEntity, PEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTracking = true)
            where TEntity : class
        {
            return await GetQueryable<TEntity>(filter, orderBy, includeProperties, skip, take, asNoTracking).AsNoTracking().ProjectTo<PEntity>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public virtual async Task<IEnumerable<PEntity>> GetAllAsync<TEntity, PEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool asNoTracking = true)
            where TEntity : class
        {
            return await GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take, asNoTracking).ProjectTo<PEntity>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public void Insert<TEntity>(TEntity entity, int user = -1) where TEntity : class, IEntity
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.CreatedById = user;
            entity.UpdatedById = user;
            _context.Add<TEntity>(entity);
        }

        public async Task<TEntity> Update<TEntity, PEntity>(Expression<Func<TEntity, bool>> predicate, PEntity entity, int user = -1) where TEntity : class, IEntity
        {
            if (entity == null) throw new ArgumentException("entity");

            var updateEntity = await _context.Set<TEntity>().SingleAsync(predicate);
            _mapper.Map(entity, updateEntity);
            updateEntity.UpdatedById = user;
            updateEntity.Updated = DateTime.Now;
            return updateEntity;
        }

        public async Task SaveAsync(int auditUser = -1) => await _context.SaveChangesAsync(user: auditUser);
    }
}
