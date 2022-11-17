using Dnw.WebShop.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dnw.WebShop.Core;

public static class ServiceExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        
        return services;
    } 
}