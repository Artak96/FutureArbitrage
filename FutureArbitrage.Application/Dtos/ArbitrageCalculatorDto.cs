namespace FutureArbitrage.Application.Dtos
{
    public record ArbitrageCalculatorDto
    {
        public TimeSpan Interval { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Asset { get; set; }
    }
}
