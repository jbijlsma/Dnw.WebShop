using JetBrains.Annotations;

namespace Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine.Models;

internal class ChannelEngineOrder
{
     public ChannelEngineOrderLine[] Lines { get; [UsedImplicitly] set; } = Array.Empty<ChannelEngineOrderLine>();
}