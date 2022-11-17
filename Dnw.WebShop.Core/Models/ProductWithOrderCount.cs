namespace Dnw.WebShop.Core.Models;

public record ProductWithOrderCount(string ProductId, string ProductName, string ProductGtin, int OrderCount);