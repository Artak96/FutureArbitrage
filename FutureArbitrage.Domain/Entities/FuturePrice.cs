using FutureArbitrage.Domain.Common;

namespace FutureArbitrage.Domain.Entities
{
    public class FuturePrice : Entity
    {
        public Guid Id { get; private set; }
        public string Symbol { get; private set; }
        public decimal Price { get; private set; }

        public FuturePrice()
        {
            
        }

        public FuturePrice(string symbol, decimal price)
        {
            Symbol = symbol;
            Price = price;
        }
    }
}
