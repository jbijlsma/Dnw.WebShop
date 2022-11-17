using JetBrains.Annotations;

namespace Dnw.WebShop.Infrastructure.ExternalApi.ChannelEngine.Models;

internal class ChannelEngineResponse<T>
{
    public T[] Content { get; [UsedImplicitly] set; } = Array.Empty<T>();
}