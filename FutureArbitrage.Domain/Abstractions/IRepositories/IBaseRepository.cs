using FutureArbitrage.Domain.Common;
using System.Linq.Expressions;

namespace FutureArbitrage.Domain.Abstractions.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(ICollection<TEntity> entities);
        Task<TEntity?> GetFristAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
        Task<TEntity?> GetFristWithNoTrackingAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
        Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken);
        Task<bool> GetAnyExistsAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
        Task<bool> GetAnyExistsWithNoTrackingAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
        Task<IReadOnlyList<TEntity>> GetAllListAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<TEntity>> GetAllListWithNoTrackingAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken);
    }
}
