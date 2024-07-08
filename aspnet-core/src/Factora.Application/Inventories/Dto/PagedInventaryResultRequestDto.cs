using Abp.Application.Services.Dto;
using System;

namespace Factora.Inventories.Dto
{
    public class PagedInventaryResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public Guid? WarehouseId { get; set; }
        public int? ProductId { get; set; }
    }
}
