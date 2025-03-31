using FutureArbitrage.Domain.Entities;

namespace FutureArbitrage.Domain.Abstractions.IRepositories
{
    public interface IArbitrageResultRepository : IBaseRepository<ArbitrageResult>
    {
        Task<List<ArbitrageResult>> GetArbitrageResultByAssetAsync(string asset);
    }
}
