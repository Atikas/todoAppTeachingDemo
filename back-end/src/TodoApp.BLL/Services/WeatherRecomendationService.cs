using TodoApp.BLL.Models;
using TodoApp.BLL.Services.Interfaces;

namespace TodoApp.BLL.Services
{
    public class WeatherRecomendationService: IWeatherRecomendationService
    {
        private readonly List<IWeatherRecommendation> _strategies;
        public WeatherRecomendationService()
        {
            //To automatically discover and instantiate classes that implement a specific interface, you can use reflection.
            //However, be aware that reflection can be slower than directly instantiating the objects.
            var type = typeof(IWeatherRecommendation);
            var types = AppDomain.CurrentDomain.GetAssemblies() //Get all loaded assemblies
                .SelectMany(s => s.GetTypes()) //Get all types in all assemblies
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface); //Filter to only include types that implement the interface and aren't the interface itself
            //Please note that this will only find classes in currently loaded assemblies. If there are other assemblies in your application that aren't loaded at the time this code runs, their types won't be included.
            
            _strategies = types.Select(t => Activator.CreateInstance(t) as IWeatherRecommendation).ToList()!;
        }

        public List<string> GenerateRecommendations(WeatherRecomendationRequest weather)
        {
            var recommendations = _strategies
                .Where(strategy => strategy.IsApplicable(weather))
                .Select(strategy => strategy.Recommendation())
                .ToList();
            return recommendations;
        }
    }



}
