using FutureArbitrage.Domain.Common;

namespace FutureArbitrage.Domain.Entities
{
    public class ArbitrageResult : Entity
    {
        public long Id { get; set; }
        public decimal PriceF1 { get; set; } 
        public decimal PriceF2 { get; set; } 
        public decimal PriceDifference { get; set; } 
        
        public long FuturesContract1Id { get; set; } 
        public virtual FutureContract? FuturesContract1 { get; set; }

        public long FuturesContract2Id { get; set; } 
        public virtual FutureContract? FuturesContract2 { get; set; }
        public DateTime Timestamp { get; set; } 

        public ArbitrageResult() { }

        public ArbitrageResult(DateTime dateTime, decimal priceF1, decimal priceF2, decimal priceDifference, long futuresContract1Id, long futuresContract2Id)
        {
            Timestamp = dateTime;
            PriceF1 = priceF1;
            PriceF2 = priceF2;  
            PriceDifference = priceDifference;
            FuturesContract1Id = futuresContract1Id;
            FuturesContract2Id = futuresContract2Id; 
        }
    }
}
