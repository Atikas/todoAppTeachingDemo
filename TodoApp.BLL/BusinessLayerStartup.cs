using Microsoft.Extensions.DependencyInjection;
using TodoApp.BLL.Services;
using TodoApp.BLL.Services.Interfaces;

namespace TodoApp.BLL;

public static class BusinessLayerStartup
{
    public static void ConfigureBusinessLayerServices(this IServiceCollection services)
    {
        services.AddTransient<IAccountService, AccountService>();
    }
}
