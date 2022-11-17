using JetBrains.Annotations;

namespace Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine.Models;

public class ChannelEngineProductPatchRequest
{
    public string[] PropertiesToUpdate { [UsedImplicitly] get; init; } = Array.Empty<string>();
    public object[] MerchantProductRequestModels { [UsedImplicitly] get; init; } = Array.Empty<object>();
}