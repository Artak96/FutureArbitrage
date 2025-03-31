namespace FutureArbitrage.Application.Models.Response
{
    public class GetArbitrageResults
    {
        public decimal PriceF1 { get; set; }
        public decimal PriceF2 { get; set; }
        public decimal PriceDifference { get; set; }
        public string Symbol1 { get; set; }
        public string Symbol2 { get; set; }
    }
}
