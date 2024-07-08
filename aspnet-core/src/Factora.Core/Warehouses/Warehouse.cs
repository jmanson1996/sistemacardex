using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Factora.Authorization.Users;
using Factora.Inventories;
using Factora.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factora.Warehouses
{
    public class Warehouse : FullAuditedAggregateRoot<Guid>, ISoftDelete
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Location { get; set; }
        public string Phone { get; set; }
        public long UserId { get; set; }
        public TypeWarehouse Type { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
        public ICollection<Order> Order { get; set; } = new List<Order>();
    }

    public enum TypeWarehouse
    {
        [Display(Name = "Main")]
        Main = 1,
        [Display(Name = "Branch")]
        Branch = 2
    }
}
