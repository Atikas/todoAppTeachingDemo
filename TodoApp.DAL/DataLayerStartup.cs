using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.DAL.Repositories;
using TodoApp.DAL.Repositories.Interfaces;

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
        }
    }
}
