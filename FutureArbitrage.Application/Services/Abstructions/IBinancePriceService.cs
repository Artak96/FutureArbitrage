using FutureArbitrage.Application.Dtos;

namespace FutureArbitrage.Application.Services.Abstructions
{
    public interface IBinancePriceService
    {
        Task<List<FuturesContractDto>> GetLatestQuarterlyContracts(string contructType);
        Task<FuturePriceDto?> GetFuturesPrice(string symbol, DateTime time, TimeSpan interval);
    }
}
