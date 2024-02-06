using TodoApp.BLL.Models;

namespace TodoApp.BLL.Services.Interfaces
{

    public interface IWeatherRecommendation
    {
        bool IsApplicable(WeatherRecomendationRequest condition);
        string Recommendation();
    }

}
