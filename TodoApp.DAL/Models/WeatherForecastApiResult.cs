namespace TodoApp.DAL.Models
{
    public class WeatherForecastApiResult
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public float elevation { get; set; }
        public WeatherForecastApiDailyUnits daily_units { get; set; }
        public WeatherForecastApiDaily daily { get; set; }
    }


}
