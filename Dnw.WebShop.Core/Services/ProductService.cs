using Dnw.WebShop.Core.Models;

namespace Dnw.WebShop.Core.Services;

public interface IProductService
{
    /// <summary>
    /// Returns the top selling products
    /// </summary>
    /// <param name="count">The number of products to return</param>
    /// <returns>A list of products with the total quantity ordered</returns>
    Task<IEnumerable<ProductOrderCount>> GetTopSellingProducts(int count);
}

public class ProductService : IProductService
{
    private readonly IChannelEngineService _channelEngineService;

    public ProductService(IChannelEngineService channelEngineService)
    {
        _channelEngineService = channelEngineService;
    }
    
    public async Task<IEnumerable<ProductOrderCount>> GetTopSellingProducts(int count)
    {
        var orders = await _channelEngineService.GetOrdersInProgress();

        var result = orders.ToList().GroupBy(order => order.ProductId).Select(group =>
        {
            var (_, productGtin, productName, _) = group.First();
            var totalQuantity = group.Sum(order => order.Quantity);

            return new ProductOrderCount(productName, productGtin, totalQuantity);
        }).OrderByDescending(product => product.TotalQuantity)
            .ThenBy(product => product.ProductName)
            .Take(count);

        return result;
    }
}