using FutureArbitrage.Application.Services.Abstructions;
using FutureArbitrage.Domain.Enums;
using FutureArbitrage.Infrastructure.Services.Implimentations;

namespace FutureArbitrage.Infrastructure.Services
{
    public class ExchangePriceServiceFactory : IExchangePriceServiceFactory
    {
        private readonly HttpClient _httpClient;

        public ExchangePriceServiceFactory(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public IExchangePriceServiceStrategy CreatePriceService(ExchangeTypeEnum exchangeType)
        {
            return exchangeType switch
            {
                ExchangeTypeEnum.Binance => new BinancePriceService(_httpClient),
                //ExchangeTypeEnum.Bybit => new BinancePriceService(_httpClient),
                _ => throw new ArgumentException($"Unsupported exchange: {exchangeType}")
            };
        }
    }
}
