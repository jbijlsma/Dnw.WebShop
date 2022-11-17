using Dnw.WebShop.Core.Models;
using Dnw.WebShop.Core.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Dnw.WebShop.Core.Tests.Services;

public class ProductServiceTests
{
    [Fact]
    public async Task GetTopSellingProducts() 
    {
        // Given
        const int count = 5;
        
        var channelEngineService = Substitute.For<IChannelEngineService>();
        var expectedOrders = new[]
        {
            new Order("p1", "Gtin1", "Product1", 2),
            new Order("p1", "Gtin1", "Product1", 3),
            new Order("p2", "Gtin2", "Product2", 2),
            new Order("p2", "Gtin2", "Product2", 4),
            new Order("p3", "Gtin3", "Product3", 1),
            new Order("p8", "Gtin8", "Product8", 2),
            new Order("p4", "Gtin4", "Product4", 2),
            new Order("p5", "Gtin5", "Product5", 1),
            new Order("p6", "Gtin6", "Product6", 4),
            new Order("p7", "Gtin7", "Product7", 8),
        };
        channelEngineService
            .GetOrdersInProgress()
            .Returns(expectedOrders);
        
        var productService = new ProductService(channelEngineService);

        // When
        var actual = await productService.GetTopSelling(count);

        // Then
        var expected = new[]
        {
            new ProductOrderCount("p7","Product7", "Gtin7", 8),
            new ProductOrderCount("p2","Product2", "Gtin2", 6),
            new ProductOrderCount("p1","Product1", "Gtin1", 5),
            new ProductOrderCount("p6","Product6", "Gtin6", 4),
            new ProductOrderCount("p4","Product4", "Gtin4", 2),
        };
        actual.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }
}