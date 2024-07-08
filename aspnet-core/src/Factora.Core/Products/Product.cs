using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Factora.Inventories;
using Factora.Orders;
using Factora.OrdersDetails;
using System.Collections.Generic;

namespace Factora.Products
{
    public class Product : FullAuditedEntity, ISoftDelete
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
        public ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
    }
}
