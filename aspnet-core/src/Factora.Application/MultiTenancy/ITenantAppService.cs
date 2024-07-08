using Abp.Application.Services;
using Factora.MultiTenancy.Dto;

namespace Factora.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

