using System.Net.Http.Json;
using Dnw.WebShop.Core.Models;
using Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine.Models;

namespace Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine;

internal interface IChannelEngineService
{
    Task<IEnumerable<OrderItem>> GetOrdersInProgress();
}

internal class ChannelEngineService : IChannelEngineService
{
    private readonly HttpClient _httpClient;

    public ChannelEngineService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<OrderItem>> GetOrdersInProgress()
    {
        var ordersResponse = await _httpClient.GetFromJsonAsync<GetOrdersResponse>("/api/v2/orders?statuses=IN_PROGRESS");

        return Map(ordersResponse!);
    }

    private static IEnumerable<OrderItem> Map(GetOrdersResponse getOrdersResponse)
    {
        var orderLines = getOrdersResponse.Content.SelectMany(order => order.Lines);
        return orderLines.Select(Map);
    }

    private static OrderItem Map(ChannelEngineOrderLine orderLine)
    {
        return new OrderItem(orderLine.MerchantProductNo, orderLine.Gtin, orderLine.Description, orderLine.Quantity);
    }
}