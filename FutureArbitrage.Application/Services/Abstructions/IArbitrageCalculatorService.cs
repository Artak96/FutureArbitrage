using FutureArbitrage.Domain.Entities;

namespace FutureArbitrage.Application.Services.Abstructions
{
    public interface IArbitrageCalculatorService
    {
        Task<List<ArbitrageResult>> CalculateArbitrage(TimeSpan interval, DateTime startTime, DateTime endTime);
    }
}
