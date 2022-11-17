using Dnw.WebShop.Core.Models;

namespace Dnw.WebShop.Core.Services;

public interface IProductService
{
    IEnumerable<ProductOrderCount> GetTopSellingProducts(int count);
}

public class ProductService : IProductService
{
    public IEnumerable<ProductOrderCount> GetTopSellingProducts(int count)
    {
        var products = new[]
        {
            new ProductOrderCount("Product1", "1", 1),
            new ProductOrderCount("Product2", "2", 2),
            new ProductOrderCount("Product3", "3", 3),
            new ProductOrderCount("Product4", "4", 4),
            new ProductOrderCount("Product5", "5", 5),
            new ProductOrderCount("Product6", "6", 6),
            new ProductOrderCount("Product7", "7", 7),
        };

        return products.OrderByDescending(p => p.TotalQuantity).Take(count);
    }
}