namespace FutureArbitrage.Application.Dtos
{
    public record FuturePriceDto
    {
        public decimal Price { get; set; } 
        public DateTime Timestamp { get; set; }
    }
}
