using FutureArbitrage.Application.Dtos;

namespace FutureArbitrage.Application.Services.Abstructions
{
    public interface IPriceServiceContextStrategy
    {
        Task<List<FuturesContractDto>> GetLatestQuarterlyContracts(string asset);
        Task<FuturePriceDto?> GetFuturesPrice(string symbol, DateTime time, TimeSpan interval);
        void SetPriceService(IExchangePriceServiceStrategy priceService); 
    }
}
