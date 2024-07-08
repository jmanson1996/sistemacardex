using Abp.Application.Services;
using Factora.OrdersDetails.Dto;

namespace Factora.OrdersDetails
{
    public interface IOrderDetailAppService : IAsyncCrudAppService<OrderDetailViewDto, int, PagedOrderDetailResultRequestDto, OrderDetailFullDto, OrderDetailViewDto>
    {
    }
}
