using Abp.Application.Services;
using Factora.Roles.Dto;
using Factora.Warehouses.Dto;
using System;

namespace Factora.Warehouses
{
    /// <summary>
    /// Interface WarehouseAppService
    /// </summary>
    public interface IWarehouseAppService : IAsyncCrudAppService<WarehouseViewDto, Guid, PagedWarehouseResultRequestDto, WarehouseFullDto, WarehouseViewDto>
    {
    }
}
