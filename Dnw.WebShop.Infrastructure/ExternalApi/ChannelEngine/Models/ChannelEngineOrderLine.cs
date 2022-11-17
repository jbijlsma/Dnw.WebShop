using JetBrains.Annotations;

namespace Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine.Models;

internal class ChannelEngineOrderLine
{
    public string Gtin { get; [UsedImplicitly] set; } = string.Empty;
    public string Description { get; [UsedImplicitly] set; } = string.Empty;
    public string MerchantProductNo { get; [UsedImplicitly] set; } = string.Empty;
    public int Quantity { get; [UsedImplicitly] set; }
}