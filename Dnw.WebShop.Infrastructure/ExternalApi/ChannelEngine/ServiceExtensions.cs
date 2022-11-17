using Dnw.WebShop.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine;

public static class ServiceExtensions
{
    public static void AddChannelEngineService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IChannelEngineService, ChannelEngineService>(client =>
        {
            var baseUrl = configuration["ChannelEngine:BaseUrl"]!;
            client.BaseAddress = new Uri(baseUrl);

            var apiKey = configuration["ChannelEngine:ApiKey"];
            client.DefaultRequestHeaders.Add("X-CE-KEY", new [] { apiKey });
        });
    }
}