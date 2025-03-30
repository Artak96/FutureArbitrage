namespace FutureArbitrage.Application.Dtos
{
    public record FuturesContractDto
    {
        public string Symbol { get; set; }
        public long DeliveryDate { get; set; }
    }
}
