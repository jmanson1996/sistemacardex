using AutoMapper;

namespace Factora.Products.Dto
{
    public class ProductProfileMapper : Profile
    {
        public ProductProfileMapper()
        {
            CreateMap<Product, ProductViewDto>();
            CreateMap<ProductViewDto, Product>();
            CreateMap<Product, ProductFullDto>();
            CreateMap<ProductFullDto, Product>();
        }
    }
}
