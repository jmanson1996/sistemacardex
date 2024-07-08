using System;
using System.ComponentModel.DataAnnotations;

namespace Factora.Warehouses.Dto
{
    public class WarehouseFullDto
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Location { get; set; }
        public string Phone { get; set; }
        public long UserId { get; set; }
        public TypeWarehouse Type { get; set; }
    }
}
