using System.Reflection;
using Dnw.WebShop.Core.Services;
using Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dnw.WebShop.Infrastructure.Tests.ExternalApi.ChannelEngine;

public class ChannelEngineServiceTests
{
    private readonly IChannelEngineService _service;
    
    public ChannelEngineServiceTests()
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .AddUserSecrets(Assembly.GetExecutingAssembly());

        var services = new ServiceCollection();
        services.AddChannelEngineService(configBuilder.Build());
        var serviceProvider = services.BuildServiceProvider();
        
        _service = serviceProvider.GetRequiredService<IChannelEngineService>();
    }

    [Fact]
    public async Task GetOrdersInProgress()
    {
        // When
        var actual = await _service.GetOrdersInProgress();

        // Then
        actual.Should().HaveCount(7);
    }
}