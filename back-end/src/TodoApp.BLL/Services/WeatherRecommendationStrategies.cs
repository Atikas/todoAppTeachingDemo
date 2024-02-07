using TodoApp.BLL.Models;
using TodoApp.BLL.Services.Interfaces;

namespace TodoApp.BLL.Services
{
    /*
  Temperature-Based Clothing Recommendations
      If Temperature < 10°C : "Wear a coat; it's going to be cold."
      If Temperature >= 10°C and Temperature < 20°C: "Wear a jacket; it's cool outside."
      If Temperature >= 20°C and Temperature < 30°C: "Wear light clothing; it's warm."
      If Temperature >= 30°C and Temperature < 40°C: "Wear very light clothing; it's hot outside."
      If Temperature >= 40°C: "Stay indoors; it's extremely hot outside."

  Precipitation-Based Recommendations
      If Precipitation > 0.0 mm and Precipitation <= 2.5 mm and Temperature > 0°C: "Light rain: Consider taking an umbrella;"
      If Precipitation > 2.5 mm and Precipitation <= 7.6 mm and Temperature > 0°C: "Moderate rain: Take an umbrella; "
      If Precipitation > 7.6 mm and Temperature > 0°C: "Heavy rain: Take an umbrella and wear waterproof shoes."

  TODO Wind-Based Recommendations
      If Wind > 20 km/h and Wind <= 40 km/h: "It's windy, consider wearing something that won't get blown around."
      If Wind > 40 km/h: "It's very windy, be cautious if you're planning to use an umbrella."

  TODO UV Index-Based Recommendations
      If UvIndex >= 3 and UvIndex < 6: "Wear sunglasses and apply SPF 30+ sunscreen."
      If UvIndex >= 6 and UvIndex < 8: "Minimize sun exposure between 10 a.m. and 4 p.m.; wear protective clothing."
      If UvIndex >= 8: "Stay indoors during midday hours if possible; apply SPF 50+ sunscreen."

  TODO Snowfall-Based Recommendations
      If Snowfall > 0 and Snowfall <= 10 mm: "Wear boots and warm clothing; light snowfall expected."
      If Snowfall > 10 mm: "Heavy snowfall expected; ensure you're well insulated."

  TODO Combined Temperature and Wind (Wind Chill)
      If Temperature < 0°C and Wind > 20 km/h: "Dress in layers and protect against wind chill."

  TODO Rain and Temperature for Cold Weather
      If Precipitation > 2.5 mm and Temperature < 10°C: "Wear waterproof & insulated clothing."

  TODO High Temperature and UV Index
      If Temperature >= 30°C and UvIndex >= 8: "Stay hydrated and avoid direct sunlight when possible."

  TODO Moderate Wind and UV Index
      If Wind > 20 km/h and UvIndex >= 3: "Apply lip balm with SPF and wear a hat to protect against wind and sun."

  TODO Early Morning or Late Evening Low Temperature
      If Temperature < 5°C, regardless of Day: "Consider warmer clothes for early morning or late evening."*/


    //---------- Temperature-Based Clothing Recommendations
    public class TemperatureBelow10Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Temperature < 10 && condition.Temperature >= -273.15;
        public string Recommendation() => "Wear a coat; it's going to be cold.";
    }
    public class TemperatureBetween10And20Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Temperature >= 10 && condition.Temperature < 20;
        public string Recommendation() => "Wear a jacket; it's cool outside.";
    }
    public class TemperatureBetween20And30Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Temperature >= 20 && condition.Temperature < 30;
        public string Recommendation() => "Wear light clothing; it's warm.";
    }
    public class TemperatureBetween30And40Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Temperature >= 30 && condition.Temperature < 40;
        public string Recommendation() => "Wear very light clothing; it's hot outside.";
    }
    public class TemperatureGreaterThan40Recomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Temperature >= 40;
        public string Recommendation() => "Stay indoors; it's extremely hot outside.";
    }


    //---------- Precipitation-Based Recommendations
    public class PrecipitationLightRecomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Precipitation > 0 && condition.Precipitation <= 2.5 && condition.Temperature > 0;
        public string Recommendation() => "Light rain: Consider taking an umbrella;";
    }
    public class PrecipitationModerateRecomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Precipitation > 2.5 && condition.Precipitation <= 7.6 && condition.Temperature > 0;
        public string Recommendation() => "Moderate rain: Take an umbrella;";
    }
    public class PrecipitationHeavyRecomendation : IWeatherRecommendation
    {
        public bool IsApplicable(WeatherRecomendationRequest condition) => condition.Precipitation > 7.6 && condition.Temperature > 0;
        public string Recommendation() => "Heavy rain: Take an umbrella and wear waterproof shoes.";
    }


   
}
