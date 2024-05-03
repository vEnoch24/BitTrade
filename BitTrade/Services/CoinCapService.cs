using BitTrade.Services;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


public class CoinCapService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey; // Add your CoinCap API key here

    public CoinCapService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["CoinCapApiKey"]; // Retrieve the API key from configuration
    }

    public async Task<decimal> GetBitcoinConversionRate()
    {
        var url = "https://api.coincap.io/v2/rates/bitcoin";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Authorization", $"Bearer {_apiKey}");

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var rateResponse = JsonSerializer.Deserialize<CoinCapRateResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return rateResponse?.Data?.RateUsd ?? 0;
        }

        return 0; // Return 0 if the request was not successful
    }
}