using FutureArbitrage.Domain.Abstractions.IRepositories;

namespace FutureArbitrage.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IFuturePriceRepository FuturePrice { get; }
        IFutureContractRepository FutureContract { get; }
        IArbitrageResultRepository ArbitrageResult { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
