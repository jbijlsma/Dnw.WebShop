using Dnw.WebShop.Core.Models;

namespace Dnw.WebShop.Core.Services;

public interface IChannelEngineService
{
    Task<IEnumerable<OrderItem>> GetOrdersInProgress();
}