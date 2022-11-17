namespace Dnw.WebShop.Core.Models;

public record OrderItem(string ProductId, string ProductGtin, string ProductName, int Quantity);