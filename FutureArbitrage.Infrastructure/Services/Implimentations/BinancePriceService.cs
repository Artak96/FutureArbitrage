using FutureArbitrage.Application.Services.Abstructions;
using FutureArbitrage.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FutureArbitrage.Infrastructure.Services.Implimentations
{
    public class BinancePriceService : IBinancePriceService
    {
        private readonly HttpClient _httpClient;
        private const string BinanceApiBaseUrl = "https://fapi.binance.com/fapi/v1/klines";
        private const string ExchangeInfoUrl = "https://fapi.binance.com/fapi/v1/exchangeInfo";

        public BinancePriceService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<(string symbol1, string symbol2)> GetLatestQuarterlyContracts()
        {
            var response = await _httpClient.GetStringAsync(ExchangeInfoUrl);
            var exchangeInfo = JObject.Parse(response); // Parse the JSON response into a JObject

            // Deserialize the "symbols" array directly to a list of FuturesContract
            var symbols = exchangeInfo["symbols"]?
                .ToObject<List<FuturesContract>>() ?? new List<FuturesContract>(); // Use safe navigation

            // Filter BTCUSDT quarterly futures (excluding perpetual contracts)
            var quarterlyContracts = symbols
                .Where(s => s.Symbol.StartsWith("BTCUSDT_") && s.Symbol.Length == 14) // YYMMDD format
                .OrderBy(s => s.DeliveryDate)
                .ToList();

            if (quarterlyContracts.Count < 2)
                throw new Exception("Not enough quarterly contracts available");

            // Return the two nearest future contracts
            return (quarterlyContracts[0].Symbol, quarterlyContracts[1].Symbol);
        }
        //public async Task<(string symbol1, string symbol2)> GetLatestQuarterlyContracts()
        //{
        //    var response = await _httpClient.GetStringAsync(ExchangeInfoUrl);
        //    var exchangeInfo = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
        //    var symbols = JsonConvert.DeserializeObject<List<FutureContract>>(exchangeInfo["symbols"].ToString());
        //   // var exchangeInfo = JObject.Parse(response);

        //   // Десериализуем символы фьючерсов
        //   //var symbols = exchangeInfo["symbols"]?.ToObject<List<FuturesContract>>() ?? new List<FuturesContract>();


        //    var quarterlyContracts = symbols
        //        .Where(s => s.Symbol.StartsWith("BTCUSDT_") && s.Symbol.Length == 14)
        //        .OrderBy(s => s.DeliveryDate)
        //        .ToList();

        //    if (quarterlyContracts.Count < 2)
        //        throw new Exception("Not enough quarterly contracts available");

        //    return (quarterlyContracts[0].Symbol, quarterlyContracts[1].Symbol);
        //}

        public async Task<FuturePrice?> GetFuturesPrice(string symbol, DateTime time, TimeSpan interval)
        {
            string intervalStr = interval.TotalHours >= 48 ? "2d" : "1h";
            long startTimeUnix = ((DateTimeOffset)time).ToUnixTimeMilliseconds();
            string url = $"{BinanceApiBaseUrl}?symbol={symbol}&interval={intervalStr}&startTime={startTimeUnix}&limit=1";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<object[][]>(response);

                if (data.Length > 0)
                {
                    return new FuturePrice
                    {
                        Timestamp = time,
                        Price = decimal.Parse(data[0][4].ToString())
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to fetch price for {symbol} at {time}: {ex.Message}");
                return null;
            }
        }
    }
    public class FuturesContract
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }  // Символ контракта (BTCUSDT_QUARTER, BTCUSDT_BI-QUARTER)
        [JsonProperty("deliveryDate")]
        public DateTime DeliveryDate { get; set; }  // Дата экспирации контракта
    }
}
