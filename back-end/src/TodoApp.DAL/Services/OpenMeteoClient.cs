using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DAL.Models;
using TodoApp.DAL.Services.Services;

namespace TodoApp.DAL.Services
{
    public class OpenMeteoClient: IOpenMeteoClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OpenMeteoClient> _logger;
        public OpenMeteoClient(IHttpClientFactory httpClientFactory, ILogger<OpenMeteoClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<WeatherForecastApiResult?> GetWeatherForecastAsync(double latitude, double longitude)
        {
            _logger.LogInformation("GetWeatherForecastAsync called with latitude: {latitude}, longitude: {longitude}", latitude, longitude);
            var httpClient = _httpClientFactory.CreateClient("WeatherForecastApi");
            var route = "forecast";
            var query = $"latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,wind_speed_10m_max";
            try
            {
                var response = await httpClient.GetAsync($"{route}?{query}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<WeatherForecastApiResult>(content);
                    return res;
                }
                else
                {
                    _logger.LogWarning("GetWeatherForecastAsync failed with latitude: {latitude}, longitude: {longitude}", latitude, longitude);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetWeatherForecastAsync failed with latitude: {latitude}, longitude: {longitude}", latitude, longitude);
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
