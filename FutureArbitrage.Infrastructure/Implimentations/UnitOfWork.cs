using FutureArbitrage.Domain.Abstractions;
using FutureArbitrage.Domain.Abstractions.IRepositories;
using FutureArbitrage.Domain.Common;
using FutureArbitrage.Infrastructure.Data.Context;
using FutureArbitrage.Infrastructure.Implimentations.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FutureArbitrage.Infrastructure.Implimentations
{
    public class UnitOfWOrk : IUnitOfWork
    {
        private readonly FutureArbitrageDbContext _dbContext;

        public UnitOfWOrk(FutureArbitrageDbContext context)
        {
            _dbContext = context;
        }

        private FuturePriceRepository _futurePrice;
        public IFuturePriceRepository FuturePrice => _futurePrice ?? new FuturePriceRepository(_dbContext);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (!_dbContext.ChangeTracker.HasChanges())
                return 0;

            var now = DateTime.UtcNow;

            foreach (var entry in _dbContext.ChangeTracker.Entries<Entity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = now;
                    entry.Entity.UpdatedDate = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(e => e.CreatedDate).IsModified = false;
                    entry.Entity.UpdatedDate = now;
                }
            }

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
