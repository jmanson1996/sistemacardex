using Abp.Application.Services.Dto;
using System;

namespace Factora.Orders.Dto
{
    public class OrderViewDto : EntityDto
    {
        public Guid WarehouseId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
