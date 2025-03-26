using FutureArbitrage.Domain.Common;

namespace FutureArbitrage.Domain.Entities
{
    public class PriceDifference : Entity
    {
        public Guid Id { get; private set; }
        public decimal Difference { get; private set; }

        public PriceDifference()
        {
        }

        public PriceDifference(decimal difference)
        {
            Id = Guid.NewGuid();
            Difference = difference;
        }
    }
}
