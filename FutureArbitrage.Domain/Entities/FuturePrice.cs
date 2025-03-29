using FutureArbitrage.Domain.Common;

namespace FutureArbitrage.Domain.Entities
{
    public class FuturePrice : Entity
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; } // Временная метка
        public decimal Price { get; set; } // Цена закрытия

        public long FutureContractId { get; set; } 
        public virtual FutureContract FuturesContract { get; set; }

        public FuturePrice() { }

        public FuturePrice(DateTime timestamp, decimal price, long futureContractId)
        {
            Timestamp = timestamp;
            Price = price;
            FutureContractId = futureContractId;
        }
    }
}
