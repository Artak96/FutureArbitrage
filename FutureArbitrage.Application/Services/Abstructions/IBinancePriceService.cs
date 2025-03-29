using FutureArbitrage.Domain.Entities;

namespace FutureArbitrage.Application.Services.Abstructions
{
    public interface IBinancePriceService
    {
        Task<(string symbol1, string symbol2)> GetLatestQuarterlyContracts();
        Task<FuturePrice?> GetFuturesPrice(string symbol, DateTime time, TimeSpan interval);
    }
}
