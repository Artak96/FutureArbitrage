using FutureArbitrage.Application.Dtos;
using FutureArbitrage.Application.Services.Abstructions;
using FutureArbitrage.Domain.Abstractions;
using FutureArbitrage.Domain.Entities;
using FutureArbitrage.Domain.Enums;
using Serilog;
using System.Globalization;

namespace FutureArbitrage.Application.Services.Implimentations
{
    public class ArbitrageCalculatorService : IArbitrageCalculatorService
    {
        protected internal readonly ILogger _logger = Log.ForContext(typeof(ArbitrageCalculatorService));
        private readonly IPriceServiceContextStrategy _priceServiceContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExchangePriceServiceFactory _factory; 
        private readonly HttpClient _httpClient;

        public ArbitrageCalculatorService(
            IPriceServiceContextStrategy priceServiceContext,
            IUnitOfWork unitOfWork,
            IExchangePriceServiceFactory factory,
            HttpClient httpClient)
        {
            _priceServiceContext = priceServiceContext;
            _unitOfWork = unitOfWork;
            _factory = factory;
            _httpClient = httpClient;
        }

        public async Task SetExchange(ExchangeTypeEnum exchangeType)
        {
            var newService = _factory.CreatePriceService(exchangeType);
            _priceServiceContext.SetPriceService(newService);
        }

        public async Task CalculateArbitrage(ArbitrageCalculatorDto arbitrageCalculatorDto, CancellationToken cancellation)
        {
            _logger.Information($"Start => {nameof(ArbitrageCalculatorService)}");

            var futureContracts = await _priceServiceContext.GetLatestQuarterlyContracts(arbitrageCalculatorDto.Asset);
            DateTime currentTime = arbitrageCalculatorDto.StartTime;
            decimal lastPrice1 = 0m;
            decimal lastPrice2 = 0m;

            for (int i = 0; i < 2; i++)
            {
                var contract = await _unitOfWork.FutureContract.GetFristAsync(f => f.Symbol == futureContracts[i].Symbol, cancellation);
                if (contract == null)
                {
                    string[] parts = futureContracts[i].Symbol.Split('_');
                    string asset = parts[0];
                    string deliveryDate = parts[1];

                    DateTime localDateTime = DateTime.ParseExact(deliveryDate, "ddMMyy", CultureInfo.InvariantCulture);
                    DateTime utcDeliveryDate = localDateTime.Kind == DateTimeKind.Unspecified
                        ? DateTime.SpecifyKind(localDateTime, DateTimeKind.Utc)
                        : localDateTime.ToUniversalTime();

                    contract = new FutureContract(futureContracts[i].Symbol, utcDeliveryDate, asset);
                    await _unitOfWork.FutureContract.AddAsync(contract, cancellation);
                    await _unitOfWork.SaveChangesAsync(cancellation);
                }
            }

            var futureContract1Id = (await _unitOfWork.FutureContract.GetFristAsync(f => f.Symbol == futureContracts[0].Symbol, cancellation))!.Id;
            var futureContract2Id = (await _unitOfWork.FutureContract.GetFristAsync(f => f.Symbol == futureContracts[1].Symbol, cancellation))!.Id;

            while (currentTime <= arbitrageCalculatorDto.EndTime)
            {
                try
                {
                    var price1Task = _priceServiceContext.GetFuturesPrice(futureContracts[0].Symbol, currentTime, arbitrageCalculatorDto.Interval);
                    var price2Task = _priceServiceContext.GetFuturesPrice(futureContracts[1].Symbol, currentTime, arbitrageCalculatorDto.Interval);

                    await Task.WhenAll(price1Task, price2Task);

                    var price1 = price1Task.Result;
                    var price2 = price2Task.Result;

                    decimal currentPrice1 = price1?.Price ?? lastPrice1;
                    decimal currentPrice2 = price2?.Price ?? lastPrice2;

                    lastPrice1 = price1?.Price ?? lastPrice1;
                    lastPrice2 = price2?.Price ?? lastPrice2;

                    var arbitrageResult = new ArbitrageResult(
                        currentTime,
                        currentPrice1,
                        currentPrice2,
                        currentPrice1 - currentPrice2,
                        futureContract1Id,
                        futureContract2Id);

                    await _unitOfWork.ArbitrageResult.AddAsync(arbitrageResult, cancellation);

                    var futurePrice1 = new FuturePrice(price1?.Timestamp ?? DateTime.UtcNow, price1?.Price ?? currentPrice1, futureContract1Id);
                    await _unitOfWork.FuturePrice.AddAsync(futurePrice1, cancellation);
                    var futurePrice2 = new FuturePrice(price2?.Timestamp ?? DateTime.UtcNow, price2?.Price ?? currentPrice2, futureContract2Id);
                    await _unitOfWork.FuturePrice.AddAsync(futurePrice2, cancellation);
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error => {nameof(ArbitrageCalculatorService)} error message => {ex.Message}");
                    throw;
                }

                currentTime = currentTime.Add(arbitrageCalculatorDto.Interval);
            }
            await _unitOfWork.SaveChangesAsync(cancellation);
            
            _logger.Information($"End => {nameof(ArbitrageCalculatorService)}");
        }
    }
}