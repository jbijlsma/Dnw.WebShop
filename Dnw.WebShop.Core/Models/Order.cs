namespace Dnw.WebShop.Core.Models;

public record Order(string ProductId, string ProductGtin, string ProductName, int Quantity);