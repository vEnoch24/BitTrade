using BitTrade.Model;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace BitTrade.Services
{
    public class CurrencyInfoService
    {
        private readonly HttpClient _httpClient;

        public CurrencyInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Dictionary<string, string>> GetCurrencies(string apiUrl)
        {
            return await _httpClient.GetFromJsonAsync<Dictionary<string, string>>(apiUrl);
        }

    }
}
