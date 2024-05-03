namespace BitTrade.Model
{
    public class ConversionRate
    {
        public Dictionary<string, decimal> Bitcoin { get; set; }
    }
    public class CoinCapRateResponse
    {
        public CoinCapRateData Data { get; set; }
    }

    public class CoinCapRateData
    {
        public decimal Rate { get; set; }
    }
}
