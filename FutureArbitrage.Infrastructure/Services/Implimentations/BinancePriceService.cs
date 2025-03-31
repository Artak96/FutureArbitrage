using FutureArbitrage.Application.Dtos;
using FutureArbitrage.Application.Services.Abstructions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace FutureArbitrage.Infrastructure.Services.Implimentations
{
    public class BinancePriceService : IExchangePriceServiceStrategy
    {
        protected internal readonly ILogger _logger = Log.ForContext(typeof(BinancePriceService));
        private readonly HttpClient _httpClient;
        private const string BinanceApiBaseUrl = "https://fapi.binance.com/fapi/v1/klines";
        private const string ExchangeInfoUrl = "https://fapi.binance.com/fapi/v1/exchangeInfo";

        public BinancePriceService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<FuturesContractDto>> GetLatestQuarterlyContracts(string asset)
        {
            _logger.Information($"Start => {nameof(GetLatestQuarterlyContracts)}");

            var response = await _httpClient.GetStringAsync(ExchangeInfoUrl);
            var exchangeInfo = JObject.Parse(response);
            var symbols = JsonConvert.DeserializeObject<List<FuturesContractDto>>(exchangeInfo["symbols"].ToString());
      
            var quarterlyContracts = symbols
                .Where(s => s.Symbol.StartsWith($"{asset}_") && s.Symbol.Length == 14) 
                .OrderBy(s => s.DeliveryDate)
                .ToList();

            if (quarterlyContracts.Count < 2)
                throw new Exception("Not enough quarterly contracts available");

            List<FuturesContractDto> futuresContracts = new List<FuturesContractDto>();
            futuresContracts.AddRange(quarterlyContracts);

            return futuresContracts;
        }

        public async Task<FuturePriceDto?> GetFuturesPrice(string symbol, DateTime time, TimeSpan interval)
        {
            _logger.Information($"Start => {nameof(GetFuturesPrice)}");

            long startTimeUnix = ((DateTimeOffset)time).ToUnixTimeMilliseconds();
            string url = $"{BinanceApiBaseUrl}?symbol={symbol}&interval={interval.TotalHours+"h"}&startTime={startTimeUnix}&limit=1";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<object[][]>(response);

                if (data.Length > 0)
                {
                    return new FuturePriceDto
                    {
                        Timestamp = time,
                        Price = decimal.Parse(data[0][4].ToString())
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error => {nameof(GetFuturesPrice)} exception message => {ex.Message}");
                return null;
            }
        }
    }
}
