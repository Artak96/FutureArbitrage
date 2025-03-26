using FutureArbitrage.Domain.Abstractions.IRepositories;
using FutureArbitrage.Domain.Entities;
using FutureArbitrage.Infrastructure.Data.Context;

namespace FutureArbitrage.Infrastructure.Implimentations.Repositories
{
    public class FuturePriceRepository : BaseRepository<FuturePrice>, IFuturePriceRepository
    {
        public FuturePriceRepository(FutureArbitrageDbContext context) : base(context)
        {
        }

        public Task<FuturePrice?> GetLatestPriceAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task SavePriceAsync(FuturePrice price)
        {
            throw new NotImplementedException();
        }
    }
}
