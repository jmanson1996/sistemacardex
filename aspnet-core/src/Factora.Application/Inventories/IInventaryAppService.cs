using Abp.Application.Services;
using Factora.Inventories.Dto;

namespace Factora.Inventories
{
    public interface IInventaryAppService : IAsyncCrudAppService<InventaryViewDto, int, PagedInventaryResultRequestDto, InventaryFullDto, InventaryViewDto>
    {
    }
}
