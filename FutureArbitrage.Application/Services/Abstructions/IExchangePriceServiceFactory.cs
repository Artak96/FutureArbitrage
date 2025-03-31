using FutureArbitrage.Domain.Enums;

namespace FutureArbitrage.Application.Services.Abstructions
{
    public interface IExchangePriceServiceFactory
    {
        IExchangePriceServiceStrategy CreatePriceService(ExchangeTypeEnum exchangeType);
    }
}
