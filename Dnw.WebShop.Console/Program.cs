// See https://aka.ms/new-console-template for more information
// Based on: https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage

using System.Reflection;
using Dnw.WebShop.Core.Services;
using Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json", false, true)
            .AddUserSecrets(Assembly.GetExecutingAssembly());

        services.AddScoped<IProductService, ProductService>();
        services.AddChannelEngineService(configBuilder.Build());
    }).Build();

using var serviceScope = host.Services.CreateScope();
var provider = serviceScope.ServiceProvider;

var productService = provider.GetRequiredService<IProductService>();
var topSellingProducts = (await productService.GetTopSellingProducts(5)).ToList(); 
Console.WriteLine();
Console.WriteLine($"Found {topSellingProducts.Count} top selling products:");
Console.WriteLine();
    
var productNames = string.Join(Environment.NewLine, topSellingProducts.Select((p, index) => $"{index+1} => {p.ProductName} ({p.TotalQuantity})"));
Console.WriteLine(productNames);
Console.WriteLine();
    
var randomIndex = Random.Shared.Next(0, topSellingProducts.Count);
var randomProduct = topSellingProducts[randomIndex];
Console.WriteLine($"Selected random product: {randomProduct.ProductName} ({randomProduct.ProductId})");
Console.WriteLine();

const int newStock = 25;
Console.WriteLine($"Updating stock to : {newStock}");

await productService.UpdateStock(randomProduct.ProductId, newStock);

Console.WriteLine($"Stock updated to : {newStock}");

Console.WriteLine();
Console.WriteLine("Press any key to exit ..");
Console.ReadLine();

await host.RunAsync();
