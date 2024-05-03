namespace BitTrade.Services
{
    public interface ICoinGeckoService
    {
        Task<decimal> GetBitcoinConversionRate(string currency);
        Task<decimal> GetBitcoinConversionRateGecko(string currency);
    }
}