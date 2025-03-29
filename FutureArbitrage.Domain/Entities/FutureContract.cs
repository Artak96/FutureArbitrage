using FutureArbitrage.Domain.Common;

namespace FutureArbitrage.Domain.Entities
{
    public class FutureContract : Entity
    {
        public long Id { get; set; }
        public string Symbol { get; set; } // Символ контракта, например, "BTCUSDT_230331"
        public DateTime DeliveryDate { get; set; } // Дата поставки (Unix timestamp или DateTime)
        public string Asset { get; set; } // Базовый актив, например, "BTCUSDT"

        public virtual ICollection<FuturePrice> FuturesPrices { get; set; }
        public virtual ICollection<ArbitrageResult> ArbitrageResultsAsContract1 { get; set; }

        public FutureContract() { }

        public FutureContract(string symbol, DateTime deliveryDate, string asset)
        {
            Symbol = symbol;
            DeliveryDate = deliveryDate;
            Asset = asset;
        }
    }
}
