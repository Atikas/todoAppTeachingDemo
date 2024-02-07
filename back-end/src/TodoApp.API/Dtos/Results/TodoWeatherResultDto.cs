namespace TodoApp.API.Dtos.Results
{
    public class TodoWeatherResultDto
    {
        /// <summary>
        /// Date of the weather forecast
        /// </summary>
        public string? Day { get; set; }

        /// <summary>
        /// TemperatureMin for the day
        /// </summary>
        public string? TemperatureMin { get; set; }

        /// <summary>
        /// TemperatureMax for the day
        /// </summary>
        public string? TemperatureMax { get; set; }

        /// <summary>
        ///  Precipitation for the day   
        /// </summary>
        public string? Precipitation { get; set; }

        /// <summary>
        /// Wind for the day
        /// </summary>
        public string? Wind { get; set; }

        /// <summary>
        /// Uv Index for the day
        /// </summary>
        public string? UvIndex { get; set; }

        /// <summary>
        /// Snowfall for the day
        /// </summary>
        public string? Snowfall { get; set; }

        /// <summary>
        /// Recommendation for the day mage by AI robot based on the weather
        /// </summary>
        public List<string>? Recommendation { get; set;}
    }
}
