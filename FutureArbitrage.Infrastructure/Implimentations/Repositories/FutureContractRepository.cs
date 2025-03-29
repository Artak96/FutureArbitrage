using FutureArbitrage.Domain.Abstractions.IRepositories;
using FutureArbitrage.Domain.Entities;
using FutureArbitrage.Infrastructure.Data.Context;

namespace FutureArbitrage.Infrastructure.Implimentations.Repositories
{
    public class FutureContractRepository : BaseRepository<FutureContract>, IFutureContractRepository
    {
        public FutureContractRepository(FutureArbitrageDbContext context) : base(context)
        {
        }
    }
}
