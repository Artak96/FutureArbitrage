using FutureArbitrage.Domain.Abstractions.IRepositories;
using FutureArbitrage.Domain.Common;
using FutureArbitrage.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FutureArbitrage.Infrastructure.Implimentations.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        protected readonly FutureArbitrageDbContext _context;
        protected DbSet<TEntity> DbSet { get; }

        public BaseRepository(FutureArbitrageDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = _context.Set<TEntity>();
        }

        public virtual void TrackEntity(TEntity entity)
        {
            if (entity != null && DbSet.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await DbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            await DbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            TrackEntity(entity);
            DbSet.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbSet.Remove(entity);
        }

        public virtual void RemoveRange(ICollection<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            DbSet.RemoveRange(entities);
        }

        public virtual async Task<TEntity?> GetFristAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return await DbSet.Where(criteria).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> GetFristWithNoTrackingAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return await DbSet.Where(criteria).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<bool> GetAnyExistsAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return await DbSet.Where(criteria).AnyAsync(cancellationToken);
        }

        public virtual async Task<bool> GetAnyExistsWithNoTrackingAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking().Where(criteria).AnyAsync(cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllListAsync(CancellationToken cancellationToken)
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllListWithNoTrackingAsync(CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return await DbSet.Where(criteria).ToListAsync(cancellationToken);
        }
    }
}
