using System.Net.Http.Json;
using Dnw.WebShop.Core.Models;
using Dnw.WebShop.Core.Services;
using Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine.Models;

namespace Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine;

internal class ChannelEngineService : IChannelEngineService
{
    private readonly HttpClient _httpClient;

    public ChannelEngineService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<OrderItem>> GetOrdersInProgress()
    {
        const string url = "/api/v2/orders?statuses=IN_PROGRESS";
        var ordersResponse = await _httpClient.GetFromJsonAsync<ChannelEngineResponse<ChannelEngineOrder>>(url);

        return Map(ordersResponse!);
    }

    public async Task UpdateProductStock(string productId, int stock)
    {
        var patchRequest = new ChannelEngineProductPatchRequest
        {
            PropertiesToUpdate = new [] { nameof(ChannelEngineProduct.Stock) },
            MerchantProductRequestModels = new object[]
            {
                new ChannelEngineProduct
                {
                    MerchantProductNo = productId,
                    Stock = stock
                }
            },
        };

        const string url = "/api/v2/products";
        var response = await _httpClient.PatchAsJsonAsync(url, patchRequest);

        response.EnsureSuccessStatusCode();
    }

    internal async Task<ChannelEngineProduct?> GetProductById(string productId)
    {
        var url = $"/api/v2/products?merchantProductNoList={productId}";
        var response = await _httpClient.GetFromJsonAsync<ChannelEngineResponse<ChannelEngineProduct>>(url);
        
        return response!.Content.FirstOrDefault();
    }

    private static IEnumerable<OrderItem> Map(ChannelEngineResponse<ChannelEngineOrder> channelEngineResponse)
    {
        var orderLines = channelEngineResponse.Content.SelectMany(order => order.Lines);
        return orderLines.Select(Map);
    }

    private static OrderItem Map(ChannelEngineOrderLine orderLine)
    {
        return new OrderItem(orderLine.MerchantProductNo, orderLine.Gtin, orderLine.Description, orderLine.Quantity);
    }
}