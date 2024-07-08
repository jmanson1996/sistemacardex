using Abp.Application.Services.Dto;
using System;

namespace Factora.Orders.Dto
{
    public class PagedOrderResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public Guid WarehouseId { get; set; }
    }
}
