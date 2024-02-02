using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DAL.Models;

namespace TodoApp.DAL.Services
{
    public interface IOpenMeteoClient
    {
        Task<GeocodingApiSearchResult?> GetGeocodingAsync(string location);
    }
    public class OpenMeteoClient: IOpenMeteoClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OpenMeteoClient> _logger;
        public OpenMeteoClient(IHttpClientFactory httpClientFactory, ILogger<OpenMeteoClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<dynamic> GetWeatherForecastAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("WeatherForecastApi");
            var route = "forecast";
            var query = "?latitude=48.8566&longitude=2.3522&current_weather=true&timezone=Europe/Paris";
            var response = await httpClient.GetAsync($"{route}?{query}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                //var res = JsonConvert.DeserializeObject<List<BookApiModel>>(content);
                //return res;
            }

            return null;
        }

        public async Task<GeocodingApiSearchResult?> GetGeocodingAsync(string location)
        {
            _logger.LogInformation("GetGeocodingAsync called with location: {location}", location);
            using var httpClient = _httpClientFactory.CreateClient("GeocodingApi");
            var route = "search";
            var query = $"name={location}&count=1&language=en&format=json";
            try
            {
                var response = await httpClient.GetAsync($"{route}?{query}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<GeocodingApiSearchResult>(content);
                    return res;
                }
                else
                {
                    _logger.LogWarning("GetGeocodingAsync failed with location: {location}", location);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetGeocodingAsync failed with location: {location}", location);
            }
            return null;
        }
    }
}
