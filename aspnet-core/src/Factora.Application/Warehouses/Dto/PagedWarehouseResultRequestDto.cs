using Abp.Application.Services.Dto;

namespace Factora.Warehouses.Dto
{
    public class PagedWarehouseResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
