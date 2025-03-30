using FutureArbitrage.Application.Dtos;

namespace FutureArbitrage.Application.Services.Abstructions
{
    public interface IArbitrageCalculatorService
    {
        Task CalculateArbitrage(ArbitrageCalculatorDto arbitrageCalculatorDto, CancellationToken cancellation);
    }
}
