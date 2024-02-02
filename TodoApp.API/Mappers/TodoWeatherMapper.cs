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
            for (int i = 0; i < item.Daily.time.Length; i++)
            {
                result.Add(new TodoWeatherResultDto
                {
                    Day = item.Daily.time[i],
                    TemperatureMin = $"{item.Daily.temperature_2m_min[i]}{item.DailyUnits.Temperature2mMin}",
                    TemperatureMax = $"{item.Daily.temperature_2m_max[i]}{item.DailyUnits.Temperature2mMax}",
                    Precipitation = $"{item.Daily.precipitation_sum[i]}{item.DailyUnits.PrecipitationSum}",
                    Wind = $"{item.Daily.wind_speed_10m_max[i]}{item.DailyUnits.WindSpeed10mMax}"
                });
            }
            return result;
           
        }
    }
}
