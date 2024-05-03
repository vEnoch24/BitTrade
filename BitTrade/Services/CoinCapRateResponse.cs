using BitTrade.Model;

namespace BitTrade.Services
{
    public class CoinCapRateResponse
    {
        public CoinCapRateData Data { get; set; }
    }

    public class CoinCapRateData
    {
        public string Symbol { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal RateUsd { get; set; }
    }
}
