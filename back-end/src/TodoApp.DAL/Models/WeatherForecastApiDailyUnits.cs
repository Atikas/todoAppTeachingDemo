using Newtonsoft.Json;

namespace TodoApp.DAL.Models
{
    public class WeatherForecastApiDailyUnits
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("temperature_2m_max")]
        public string Temperature2mMax { get; set; }

        [JsonProperty("temperature_2m_min")]
        public string Temperature2mMin { get; set; }

        [JsonProperty("precipitation_sum")]
        public string PrecipitationSum { get; set; }

        [JsonProperty("wind_speed_10m_max")]
        public string WindSpeed10mMax { get; set; }

        [JsonProperty("uv_index_max")]
        public string UvIndexMax { get; set; }

        [JsonProperty("snowfall_sum")]
        public string SnowfallSum { get; set; }
    }


}
