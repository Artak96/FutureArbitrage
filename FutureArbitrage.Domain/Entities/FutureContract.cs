using FutureArbitrage.Domain.Common;

namespace FutureArbitrage.Domain.Entities
{
    public class FutureContract : Entity
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public string Asset { get; set; }
        public DateTime DeliveryDate { get; set; }

        public virtual ICollection<FuturePrice>? FuturesPrices { get; set; }
        public virtual ICollection<ArbitrageResult>? ArbitrageResultsAsContract1 { get; set; }

        public FutureContract() { }

        public FutureContract(string symbol, DateTime deliveryDate, string asset)
        {
            Symbol = symbol;
            DeliveryDate = deliveryDate;
            Asset = asset;
        }
    }
}
