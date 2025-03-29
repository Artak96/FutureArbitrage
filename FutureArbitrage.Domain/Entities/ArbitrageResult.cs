using FutureArbitrage.Domain.Common;

namespace FutureArbitrage.Domain.Entities
{
    public class ArbitrageResult : Entity
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; } // Временная метка
        public decimal PriceF1 { get; set; } // Цена первого контракта
        public decimal PriceF2 { get; set; } // Цена второго контракта
        public decimal PriceDifference { get; set; } // Разница цен (PriceF1 - PriceF2)
        
        public long FuturesContract1Id { get; set; } // Внешний ключ на первый контракт
        public virtual FutureContract FuturesContract1 { get; set; }

        public long FuturesContract2Id { get; set; } // Внешний ключ на второй контракт
        public virtual FutureContract FuturesContract2 { get; set; }

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
