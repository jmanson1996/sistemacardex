using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Factora.Authorization.Roles;
using Factora.Authorization.Users;
using Factora.MultiTenancy;
using Factora.Warehouses;
using Factora.Products;
using Factora.Inventories;
using Factora.Orders;
using Factora.OrdersDetails;

namespace Factora.EntityFrameworkCore
{
    public class FactoraDbContext : AbpZeroDbContext<Tenant, Role, User, FactoraDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public FactoraDbContext(DbContextOptions<FactoraDbContext> options)
            : base(options)
        {
        }
    }
}
