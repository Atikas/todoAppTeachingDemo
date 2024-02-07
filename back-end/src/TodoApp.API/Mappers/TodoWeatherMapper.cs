using TodoApp.API.Dtos.Results;
using TodoApp.BLL.Models;
using TodoApp.BLL.Services;
using TodoApp.BLL.Services.Interfaces;
using TodoApp.DAL.Models;

namespace TodoApp.API.Mappers
{
    public interface ITodoWeatherMapper
    {
        List<TodoWeatherResultDto> Map(WeatherForecastApiResult item);
    }
    public class TodoWeatherMapper: ITodoWeatherMapper
    {
        private readonly IWeatherRecomendationService _service;

        public TodoWeatherMapper(IWeatherRecomendationService service)
        {
            _service = service;
        }

        public List<TodoWeatherResultDto> Map(WeatherForecastApiResult item)
        {
            var result = new List<TodoWeatherResultDto>();
            for (int i = 0; i < item.Daily.time.Length; i++)
            {
                var recommendationRequest = new WeatherRecomendationRequest(
                    item.Daily.temperature_2m_max[i],
                    item.Daily.precipitation_sum[i]);

                result.Add(new TodoWeatherResultDto
                {
                    Day = item.Daily.time[i],
                    TemperatureMin = $"{item.Daily.temperature_2m_min[i]}{item.DailyUnits.Temperature2mMin}",
                    TemperatureMax = $"{item.Daily.temperature_2m_max[i]}{item.DailyUnits.Temperature2mMax}",
                    Precipitation = $"{item.Daily.precipitation_sum[i]}{item.DailyUnits.PrecipitationSum}",
                    Wind = $"{item.Daily.wind_speed_10m_max[i]}{item.DailyUnits.WindSpeed10mMax}",
                    UvIndex = $"{item.Daily.uv_index_max[i]}{item.DailyUnits.UvIndexMax}",
                    Snowfall = $"{item.Daily.snowfall_sum[i]}{item.DailyUnits.SnowfallSum}",
                    Recommendation = _service.GenerateRecommendations(recommendationRequest)
                });
            }
            return result;
           
        }
    }
}
