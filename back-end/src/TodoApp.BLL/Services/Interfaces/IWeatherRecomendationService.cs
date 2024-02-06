using TodoApp.BLL.Models;

namespace TodoApp.BLL.Services.Interfaces
{
    public interface IWeatherRecomendationService
    {
        List<string> GenerateRecommendations(WeatherRecomendationRequest weather);
    }



}
