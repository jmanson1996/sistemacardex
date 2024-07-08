using Abp.Application.Services.Dto;

namespace Factora.OrdersDetails.Dto
{
    public class OrderDetailViewDto : EntityDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
