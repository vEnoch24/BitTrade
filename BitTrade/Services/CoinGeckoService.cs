using BitTrade.Model;
using System.Net.Http;
using System.Text.Json;

namespace BitTrade.Services
{
    public class CoinGeckoService : ICoinGeckoService
    {
        private readonly string _apiKey;
        public CoinGeckoService(IConfiguration configuration)
        {
            _apiKey = configuration["CoinCapApiKey:ApiKey"]; // Retrieve the API key from configuration
        }

        //Coin Gecko
        public async Task<decimal> GetBitcoinConversionRateGecko(string currency)
        {
            var httpClient = new HttpClient();
        
            //var response = await httpClient.GetFromJsonAsync<ConversionRate>($"http://api.exchangerate.host/live?access_key=559d8d1ca27ebf8009b359ddc07ec33b/price?ids=bitcoin&vs_currencies={currency.ToLower()}");
            var response = await httpClient.GetFromJsonAsync<ConversionRate>($"https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies={currency.ToLower()}");
            if (response?.Bitcoin != null && response.Bitcoin.TryGetValue(currency.ToLower(), out var bitcoinRate))
            {
                return bitcoinRate;
            }

            return 0;   
        }


        //Coin Cap
        public async Task<decimal> GetBitcoinConversionRate(string currency)
        {
            var httpClient = new HttpClient();
            var url = $"https://api.coincap.io/v2/rates/bitcoin?convert={currency.ToUpper()}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var rateResponse = JsonSerializer.Deserialize<CoinCapRateResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                //return rateResponse?.Data?.Rate ?? 0;00+
            }

            return 0; // Return 0 if the request was not successful
        }

    }
}
