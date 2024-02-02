using Newtonsoft.Json;

namespace TodoApp.DAL.Models
{
    public class WeatherForecastApiResult
    {
        [JsonProperty("latitude")]
        public float Latitude { get; set; }

        [JsonProperty("longitude")]
        public float Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public float Generationtime_ms { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string TimezoneAbbreviation { get; set; }

        [JsonProperty("elevation")]
        public double Elevation { get; set; }

        [JsonProperty("daily_units")]
        public WeatherForecastApiDailyUnits DailyUnits { get; set; }

        [JsonProperty("daily")]
        public WeatherForecastApiDaily Daily { get; set; }
    }


}
