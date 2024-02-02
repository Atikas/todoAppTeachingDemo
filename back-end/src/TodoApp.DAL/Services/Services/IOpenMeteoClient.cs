using TodoApp.DAL.Models;

namespace TodoApp.DAL.Services.Services
{
    public interface IOpenMeteoClient
    {
        Task<GeocodingApiSearchResult?> GetGeocodingAsync(string location);
        Task<WeatherForecastApiResult?> GetWeatherForecastAsync(double latitude, double longitude);
    }
}
