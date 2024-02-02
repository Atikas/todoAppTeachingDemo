using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using TodoApp.DAL.Repositories;
using TodoApp.DAL.Repositories.Interfaces;
using TodoApp.DAL.Services;
using TodoApp.DAL.Services.Services;

namespace TodoApp.DAL
{
    public static class DataLayerStartup
    {
        public static void ConfigureDataLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoAppContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IOpenMeteoClient, OpenMeteoClient>();

            services.AddHttpClient("WeatherForecastApi", client =>
            {
                client.BaseAddress = new Uri(configuration["Intergations:OpenMeteo:WeatherForecastUrl"]!);
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "TodoAppDemo");
                //client.DefaultRequestHeaders.Add("X-Api-Key", configuration["Intergations:OpenMeteo:ApiKey"]!);
            });

            services.AddHttpClient("GeocodingApi", client =>
            {
                client.BaseAddress = new Uri(configuration["Intergations:OpenMeteo:GeocoddingUrl"]!);
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "TodoAppDemo");
                //client.DefaultRequestHeaders.Add("X-Api-Key", configuration["Intergations:OpenMeteo:ApiKey"]!);
            });
        }
    }
}
