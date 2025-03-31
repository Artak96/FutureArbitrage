using FutureArbitrage.Application.Models.Response;
using FutureArbitrage.Domain.Abstractions.IRepositories;
using FutureArbitrage.Domain.Entities;
using FutureArbitrage.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FutureArbitrage.Infrastructure.Implimentations.Repositories
{
    public class ArbitrageResultRepository : BaseRepository<ArbitrageResult>, IArbitrageResultRepository
    {
        public ArbitrageResultRepository(FutureArbitrageDbContext context) : base(context)
        {
        }

        public async Task<List<ArbitrageResult>> GetArbitrageResultByAssetAsync(string asset)
        {
            return await _context.ArbitrageResults
                .Include(ar => ar.FuturesContract1)
                .Include(ar => ar.FuturesContract2)
                .ToListAsync();
        }
    }
}
