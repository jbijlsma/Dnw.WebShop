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
            new OrderItem("p1", "Gtin1", "Product1", 2),
            new OrderItem("p1", "Gtin1", "Product1", 3),
            new OrderItem("p2", "Gtin2", "Product2", 2),
            new OrderItem("p2", "Gtin2", "Product2", 4),
            new OrderItem("p3", "Gtin3", "Product3", 1),
            new OrderItem("p8", "Gtin8", "Product8", 2),
            new OrderItem("p4", "Gtin4", "Product4", 2),
            new OrderItem("p5", "Gtin5", "Product5", 1),
            new OrderItem("p6", "Gtin6", "Product6", 4),
            new OrderItem("p7", "Gtin7", "Product7", 8),
        };
        channelEngineService
            .GetOrdersInProgress()
            .Returns(expectedOrders);
        
        var productService = new ProductService(channelEngineService);

        // When
        var actual = await productService.GetTopSellingProducts(count);

        // Then
        var expected = new[]
        {
            new ProductOrderCount("Product7", "Gtin7", 8),
            new ProductOrderCount("Product2", "Gtin2", 6),
            new ProductOrderCount("Product1", "Gtin1", 5),
            new ProductOrderCount("Product6", "Gtin6", 4),
            new ProductOrderCount("Product4", "Gtin4", 2),
        };
        actual.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }
}