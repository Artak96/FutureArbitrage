using FutureArbitrage.Domain.Entities;

namespace FutureArbitrage.Domain.Abstractions.IRepositories
{
    public interface IFuturePriceRepository : IBaseRepository<FuturePrice>
    {
        Task<FuturePrice?> GetLatestPriceAsync(string symbol);
        Task SavePriceAsync(FuturePrice price);
    }
}
