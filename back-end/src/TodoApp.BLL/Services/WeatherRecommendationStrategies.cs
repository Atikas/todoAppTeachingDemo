using TodoApp.BLL.Models;
using TodoApp.BLL.Services.Interfaces;

namespace TodoApp.BLL.Services
{
    /*
  Temperature-Based Clothing Recommendations

      If TemperatureMax < -10°C: "Wear a very warm down jacket; it's going to be very cold."
      If TemperatureMax < 10°C and TemperatureMax < -10°C:: "Wear a coat; it's going to be cold."
      If TemperatureMax >= 10°C and TemperatureMax < 20°C: "Wear a jacket; it's cool outside."
      If TemperatureMax >= 20°C and TemperatureMax < 30°C: "Wear light clothing; it's warm."
      If TemperatureMax >= 30°C and TemperatureMax < 40°C: "Wear very light clothing; it's hot outside."
      If TemperatureMax >= 40°C: "Stay indoors; it's extremely hot outside."

  Precipitation-Based Recommendations
      If Precipitation > 0 and Precipitation <= 2.5 mm: "Consider taking an umbrella; there might be light rain."
      If Precipitation > 2.5 mm: "Take an umbrella; expect rain."

  Wind-Based Recommendations
      If Wind > 20 km/h and Wind <= 40 km/h: "It's windy, consider wearing something that won't get blown around."
      If Wind > 40 km/h: "It's very windy, be cautious if you're planning to use an umbrella."

  UV Index-Based Recommendations
      If UvIndex >= 3 and UvIndex < 6: "Wear sunglasses and apply SPF 30+ sunscreen."
      If UvIndex >= 6 and UvIndex < 8: "Minimize sun exposure between 10 a.m. and 4 p.m.; wear protective clothing."
      If UvIndex >= 8: "Stay indoors during midday hours if possible; apply SPF 50+ sunscreen."

  Snowfall-Based Recommendations
      If Snowfall > 0 and Snowfall <= 10 mm: "Wear boots and warm clothing; light snowfall expected."
      If Snowfall > 10 mm: "Heavy snowfall expected; ensure you're well insulated."

  TODO Combined Temperature and Wind (Wind Chill)
      If TemperatureMax < 0°C and Wind > 20 km/h: "Dress in layers and protect against wind chill."

  TODO Rain and Temperature for Cold Weather
      If Precipitation > 2.5 mm and TemperatureMax < 10°C: "Wear waterproof & insulated clothing."

  TODO High Temperature and UV Index
      If TemperatureMax >= 30°C and UvIndex >= 8: "Stay hydrated and avoid direct sunlight when possible."

  TODO Moderate Wind and UV Index
      If Wind > 20 km/h and UvIndex >= 3: "Apply lip balm with SPF and wear a hat to protect against wind and sun."

  TODO Early Morning or Late Evening Low Temperature
      If TemperatureMax < 5°C, regardless of Day: "Consider warmer clothes for early morning or late evening."*/

    public class TemperatureMaxBetweenMinus80AndMinus10Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.TemperatureMax > -80 && condition.TemperatureMax < -10;
        public string Recommendation() => "Wear a very warm down jacket; it's going to be very cold.";

    }
    public class TemperatureMaxBetweenMinus10And10Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.TemperatureMax >= -10 && condition.TemperatureMax < 10 ;
        public string Recommendation() => "Wear a coat; it's going to be cold.";
    }
    public class TemperatureMaxBetween10And20Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.TemperatureMax >= 10 && condition.TemperatureMax < 20;
        public string Recommendation() => "Wear a jacket; it's cool outside.";
    }
    public class TemperatureMaxBetween20And30Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.TemperatureMax >= 20 && condition.TemperatureMax < 30;
        public string Recommendation() => "Wear light clothing; it's warm.";
    }
    public class TemperatureMaxBetween30And40Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.TemperatureMax >= 30 && condition.TemperatureMax < 40;
        public string Recommendation() => "Wear very light clothing; it's hot outside.";
    }
    public class TemperatureMaxGreaterThan40Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.TemperatureMax >= 40;
        public string Recommendation() => "Stay indoors; it's extremely hot outside.";
    }


    public class PrecipitationGreaterThan0AndLessThan25Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Precipitation > 0 && condition.Precipitation <= 2.5;
        public string Recommendation() => "Consider taking an umbrella; there might be light rain.";
    }
    public class PrecipitationGreaterThan25Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Precipitation > 2.5;
        public string Recommendation() => "Take an umbrella; expect rain.";
    }


    public class WindGreaterThan20AndLessThan40Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Wind > 20 && condition.Wind <= 40;
        public string Recommendation() => "It's windy, consider wearing something that won't get blown around.";
    }
    public class WindGreaterThan40Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Wind > 40;
        public string Recommendation() => "It's very windy, be cautious if you're planning to use an umbrella.";
    }


    public class UvIndexGreaterThan3AndLessThan6Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.UvIndex >= 3 && condition.UvIndex < 6;
        public string Recommendation() => "Wear sunglasses and apply SPF 30+ sunscreen.";
    }
    public class UvIndexGreaterThan6AndLessThan8Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.UvIndex >= 6 && condition.UvIndex < 8;
        public string Recommendation() => "Minimize sun exposure between 10 a.m. and 4 p.m.; wear protective clothing.";
    }
    public class UvIndexGreaterThan8Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.UvIndex >= 8;
        public string Recommendation() => "Stay indoors during midday hours if possible; apply SPF 50+ sunscreen.";
    }


    public class SnowfallGreaterThan0AndLessThan10Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Snowfall > 0 && condition.Snowfall <= 10;
        public string Recommendation() => "Wear boots and warm clothing; light snowfall expected.";
    }
    public class SnowfallGreaterThan10Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Snowfall > 10;
        public string Recommendation() => "Heavy snowfall expected; ensure you're well insulated.";
    }
}
