using FutureArbitrage.Application.Dtos;
using FutureArbitrage.Application.Services.Abstructions;

namespace FutureArbitrage.Infrastructure.Services.Implimentations
{
    public class PriceServiceContextStrategy : IPriceServiceContextStrategy
    {
        private IExchangePriceServiceStrategy _priceService;

        public PriceServiceContextStrategy(IExchangePriceServiceStrategy priceService)
        {
            _priceService = priceService ?? throw new ArgumentNullException(nameof(priceService));
        }

        public void SetPriceService(IExchangePriceServiceStrategy priceService)
        {
            _priceService = priceService ?? throw new ArgumentNullException(nameof(priceService));
        }

        public Task<List<FuturesContractDto>> GetLatestQuarterlyContracts(string asset)
        {
            return _priceService.GetLatestQuarterlyContracts(asset);
        }

        public Task<FuturePriceDto?> GetFuturesPrice(string symbol, DateTime time, TimeSpan interval)
        {
            return _priceService.GetFuturesPrice(symbol, time, interval);
        }
    }
}
