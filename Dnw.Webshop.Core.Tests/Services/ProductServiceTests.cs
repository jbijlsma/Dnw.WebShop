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
        const int numberOfProductsToReturn = 5;
        
        var channelEngineService = Substitute.For<IChannelEngineService>();
        var expectedOrders = new[]
        {
            CreateOrder(1, 2),
            CreateOrder(1, 3),
            CreateOrder(2, 2),
            CreateOrder(2, 4),
            CreateOrder(3, 1),
            CreateOrder(8, 2),
            CreateOrder(4, 2),
            CreateOrder(5, 1),
            CreateOrder(6, 4),
            CreateOrder(7, 8)
        };
        channelEngineService
            .GetOrdersInProgress()
            .Returns(expectedOrders);
        
        var productService = new ProductService(channelEngineService);

        // When
        var actual = await productService.GetTopSelling(numberOfProductsToReturn);

        // Then
        var expected = new[]
        {
            CreateProductWithOrderCount(7, 8),
            CreateProductWithOrderCount(2, 6),
            CreateProductWithOrderCount(1, 5),
            CreateProductWithOrderCount(6, 4),
            CreateProductWithOrderCount(4, 2),
        };
        actual.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    private static Order CreateOrder(int productId, int quantity)
    {
        return new Order(productId.ToString(), $"Gtin{productId}", $"Product{productId}", quantity);
    }

    private static ProductWithOrderCount CreateProductWithOrderCount(int productId, int expectedOrderCount)
    {
        return new ProductWithOrderCount(productId.ToString(), $"Product{productId}", $"Gtin{productId}", expectedOrderCount);
    }
}