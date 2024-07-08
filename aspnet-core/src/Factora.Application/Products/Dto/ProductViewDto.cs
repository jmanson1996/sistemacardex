using Abp.Application.Services.Dto;
using System;

namespace Factora.Products.Dto
{
    public class ProductViewDto : EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
