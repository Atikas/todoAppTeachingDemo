using TodoApp.API.Dtos.Results;
using TodoApp.DAL.Models;

namespace TodoApp.API.Mappers
{
    public interface ITodoWeatherMapper
    {
        List<TodoWeatherResultDto> Map(WeatherForecastApiResult item);
    }
    public class TodoWeatherMapper: ITodoWeatherMapper
    {
        public List<TodoWeatherResultDto> Map(WeatherForecastApiResult item)
        {
            var result = new List<TodoWeatherResultDto>();
            for (int i = 0; i < item.daily.time.Length; i++)
            {
                result.Add(new TodoWeatherResultDto
                {
                    Day = item.daily.time[i],
                    TemperatureMin = $"{item.daily.temperature_2m_min[i]}{item.daily_units.temperature_2m_min}",
                    TemperatureMax = $"{item.daily.temperature_2m_max[i]}{item.daily_units.temperature_2m_max}",
                    Precipitation = $"{item.daily.precipitation_sum[i]}{item.daily_units.precipitation_sum}",
                    Wind = $"{item.daily.wind_speed_10m_max[i]}{item.daily_units.wind_speed_10m_max}"
                });
            }
            return result;
           
        }
    }
}
