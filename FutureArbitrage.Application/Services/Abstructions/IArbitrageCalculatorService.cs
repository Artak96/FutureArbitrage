using FutureArbitrage.Application.Dtos;
using FutureArbitrage.Domain.Enums;

namespace FutureArbitrage.Application.Services.Abstructions
{
    public interface IArbitrageCalculatorService
    {
        Task CalculateArbitrage(ArbitrageCalculatorDto arbitrageCalculatorDto, CancellationToken cancellation);
        Task SetExchange(ExchangeTypeEnum exchangeType);
    }
}
