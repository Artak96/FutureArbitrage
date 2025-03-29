using FutureArbitrage.Domain.Abstractions.IRepositories;
using FutureArbitrage.Domain.Common;
using FutureArbitrage.Infrastructure.Data.Context;

namespace FutureArbitrage.Infrastructure.Implimentations.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly FutureArbitrageDbContext _context;

        public BaseRepository(FutureArbitrageDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.AddAsync(entity, cancellationToken);
        }
    }
}
