using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.UI;
using Factora.Inventories;
using Factora.Products.Dto;
using Factora.Warehouses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Factora.Products
{
    public class ProductAppService : AsyncCrudAppService<Product, ProductViewDto, int, PagedProductResultRequestDto, ProductFullDto, ProductViewDto>, IProductAppService
    {
        private readonly IRepository<Inventory, int> _inventoryRepository;
        private readonly IRepository<Warehouse, Guid> _warehouseRepository;
        private readonly IRepository<Product, int> _productRepository;
        public ProductAppService(IRepository<Product, int> repository,
                                 IRepository<Warehouse, Guid> warehouseRepository,
                                 IRepository<Inventory, int> inventoryRepository) : base(repository)
        {
            _inventoryRepository = inventoryRepository;
            _warehouseRepository = warehouseRepository;
            _productRepository = repository;
        }

        public async Task AddProductToMainWarehouse(AddProductToWarehouseDto input)
        {
            var mainWarehouse = await _warehouseRepository.FirstOrDefaultAsync(x => x.Type == TypeWarehouse.Main);

            if (mainWarehouse == null)
            {
                throw new UserFriendlyException("Main warehouse not found.");
            }

            var inventory = await _inventoryRepository.FirstOrDefaultAsync(x => x.WarehouseId == mainWarehouse.Id && x.ProductId == input.ProductId);

            if (inventory != null)
            {
                inventory.CurrencyQuantity += input.Quantity;
            }
            else
            {
                await _inventoryRepository.InsertAsync(new Inventory
                {
                    WarehouseId = mainWarehouse.Id,
                    ProductId = input.ProductId,
                    CurrencyQuantity = input.Quantity
                });
            }
        }

        protected override IQueryable<Product> CreateFilteredQuery(PagedProductResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
                || x.Description.Contains(input.Keyword));
        }

        protected override IQueryable<Product> ApplySorting(IQueryable<Product> query, PagedProductResultRequestDto input)
        {
            return query.OrderBy(r => r.Name);
        }

        protected override ProductViewDto MapToEntityDto(Product warehouse)
        {
            var warehouseDto = base.MapToEntityDto(warehouse);
            return warehouseDto;
        }

        protected override Product MapToEntity(ProductFullDto createInput)
        {
            var warehouse = ObjectMapper.Map<Product>(createInput);
            return warehouse;
        }

        protected override void MapToEntity(ProductViewDto input, Product user)
        {
            ObjectMapper.Map(input, user);
        }

        public override async Task<ProductViewDto> UpdateAsync(ProductViewDto input)
        {
            try
            {
                CheckUpdatePermission();

                var entity = await GetProductByIdAsync(input.Id);
                ObjectMapper.Map(input, entity);
                return MapToEntityDto(entity);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        protected virtual async Task<Product> GetProductByIdAsync(int productId)
        {
            var role = await _productRepository.FirstOrDefaultAsync(x => x.Id == productId);
            if (role == null)
            {
                throw new AbpException("There is no Produt with id: " + productId);
            }

            return role;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
