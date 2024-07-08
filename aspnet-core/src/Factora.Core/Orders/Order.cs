using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Factora.OrdersDetails;
using Factora.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factora.Orders
{
    public class Order : FullAuditedAggregateRoot<int>, ISoftDelete
    {
        public Guid WarehouseId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
    }
    public enum OrderStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3
    }
}
