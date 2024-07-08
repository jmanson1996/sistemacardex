using Abp.Application.Services.Dto;

namespace Factora.OrdersDetails.Dto
{
    public class PagedOrderDetailResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int? OrderDetail { get; set; }
    }
}
