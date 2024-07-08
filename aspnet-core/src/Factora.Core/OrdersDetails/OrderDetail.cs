using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Factora.Orders;
using Factora.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factora.OrdersDetails
{
    public class OrderDetail : FullAuditedAggregateRoot<int>, ISoftDelete
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
