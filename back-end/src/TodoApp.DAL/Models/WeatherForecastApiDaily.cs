namespace TodoApp.DAL.Models
{
    public class WeatherForecastApiDaily
    {
        public string[] time { get; set; }
        public double[] temperature_2m_max { get; set; }
        public double[] temperature_2m_min { get; set; }
        public double[] precipitation_sum { get; set; }
        public double[] wind_speed_10m_max { get; set; }
    }


}
