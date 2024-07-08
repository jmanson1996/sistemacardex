using Abp.Application.Services;
using Factora.Products.Dto;

namespace Factora.Products
{
    public interface IProductAppService : IAsyncCrudAppService<ProductViewDto, int, PagedProductResultRequestDto, ProductFullDto, ProductViewDto>
    {
    }
}
