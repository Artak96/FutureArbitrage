using FutureArbitrage.Application.Services.Abstructions;
using FutureArbitrage.Domain.Entities;

namespace FutureArbitrage.Application.Services.Implimentations
{
    public class ArbitrageCalculatorService : IArbitrageCalculatorService
    {
        private readonly IBinancePriceService _binanceService;
        private readonly List<ArbitrageResult> _results = new();

        public ArbitrageCalculatorService(IBinancePriceService binanceService)
        {
            _binanceService = binanceService;
        }

        public async Task<List<ArbitrageResult>> CalculateArbitrage(TimeSpan interval, DateTime startTime, DateTime endTime)
        {
            var (futures1Symbol, futures2Symbol) = await _binanceService.GetLatestQuarterlyContracts();
            DateTime currentTime = startTime;
            decimal lastPrice1 = 0m;
            decimal lastPrice2 = 0m;

            while (currentTime <= endTime)
            {
                try
                {
                    var price1Task = _binanceService.GetFuturesPrice(futures1Symbol, currentTime, interval);
                    var price2Task = _binanceService.GetFuturesPrice(futures2Symbol, currentTime, interval);

                    await Task.WhenAll(price1Task, price2Task);
                    var price1 = price1Task.Result;
                    var price2 = price2Task.Result;

                    decimal currentPrice1 = price1?.Price ?? lastPrice1;
                    decimal currentPrice2 = price2?.Price ?? lastPrice2;
                    if (price1 != null) lastPrice1 = currentPrice1;
                    if (price2 != null) lastPrice2 = currentPrice2;

                    _results.Add(new ArbitrageResult
                    {
                        Timestamp = currentTime,
                        PriceF1 = currentPrice1,
                        PriceF2 = currentPrice2,
                        PriceDifference = currentPrice1 - currentPrice2
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {currentTime}: {ex.Message}");
                }
                currentTime = currentTime.Add(interval);
            }
            return _results;
        }
    }

}
