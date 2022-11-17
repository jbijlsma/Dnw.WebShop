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
    private readonly ChannelEngineService _service;
    
    public ChannelEngineServiceTests()
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .AddUserSecrets(Assembly.GetExecutingAssembly());

        var services = new ServiceCollection();
        services.AddChannelEngineService(configBuilder.Build());
        var serviceProvider = services.BuildServiceProvider();
        
        _service = (ChannelEngineService)serviceProvider.GetRequiredService<IChannelEngineService>();
    }

    [Fact]
    public async Task GetOrdersInProgress()
    {
        // When
        var actual = await _service.GetOrdersInProgress();

        // Then
        actual.Should().HaveCount(7);
    }

    [Fact]
    public async Task UpdateProductStock()
    {
        // Given
        const string productId = "001201-S";
        const int stock = 25;
        
        var existingProduct = await _service.GetProductById(productId);
        var existingProductStock = existingProduct!.Stock;
        
        // When
        await _service.UpdateProductStock(productId, stock);
        
        // Then
        var actual = await _service.GetProductById(productId);
        actual!.Stock.Should().Be(stock);
        
        // Finally
        await _service.UpdateProductStock(productId, existingProductStock);
    }

    [Fact]
    public async Task GetProductById()
    {
        // Given
        const string productId = "001201-S";
        
        // When
        var actual = await _service.GetProductById(productId);
        
        // Then
        actual!.MerchantProductNo.Should().Be(productId);
    }
}