using FutureArbitrage.Domain.Abstractions.IRepositories;

namespace FutureArbitrage.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IFuturePriceRepository FuturePrice { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
