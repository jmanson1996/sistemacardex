using Abp.Application.Services.Dto;
using Factora.Products.Dto;
using System;

namespace Factora.Inventories.Dto
{
    public class InventaryViewDto : EntityDto
    {
        public Guid WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int CurrencyQuantity { get; set; }
        public ProductViewDto Product { get; set; }
    }
}
