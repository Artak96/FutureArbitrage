using FutureArbitrage.Domain.Abstractions.IRepositories;
using FutureArbitrage.Domain.Entities;
using FutureArbitrage.Infrastructure.Data.Context;

namespace FutureArbitrage.Infrastructure.Implimentations.Repositories
{
    public class ArbitrageResultRepository : BaseRepository<ArbitrageResult>, IArbitrageResultRepository
    {
        public ArbitrageResultRepository(FutureArbitrageDbContext context) : base(context)
        {
        }
    }
}
