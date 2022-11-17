using JetBrains.Annotations;

namespace Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine.Models;

internal class GetOrdersResponse
{
    public ChannelEngineOrder[] Content { get; [UsedImplicitly] set; } = Array.Empty<ChannelEngineOrder>();
}