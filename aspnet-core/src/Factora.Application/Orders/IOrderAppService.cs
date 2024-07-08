using Abp.Application.Services;
using Factora.Orders.Dto;

namespace Factora.Orders
{
    public interface IOrderAppService : IAsyncCrudAppService<OrderViewDto, int, PagedOrderResultRequestDto, OrderFullDto, OrderViewDto>
    {
    }
}
