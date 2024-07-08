using System;

namespace Factora.Inventories.Dto
{
    public class InventaryFullDto
    {
        public Guid WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int CurrencyQuantity { get; set; }
    }
}
