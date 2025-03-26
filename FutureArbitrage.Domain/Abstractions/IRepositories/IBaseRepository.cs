using FutureArbitrage.Domain.Common;

namespace FutureArbitrage.Domain.Abstractions.IRepositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task AddAsync(T entity);
    }
}
