using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Factora.Products;
using Factora.Warehouses;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factora.Inventories
{
    public class Inventory : FullAuditedAggregateRoot<int>, ISoftDelete
    {
        public Guid WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int CurrencyQuantity { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
