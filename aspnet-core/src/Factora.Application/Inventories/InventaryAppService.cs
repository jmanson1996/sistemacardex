using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.UI;
using Factora.Inventories.Dto;
using Factora.Products;
using Factora.Products.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Factora.Inventories
{
    public class InventaryAppService : AsyncCrudAppService<Inventory, InventaryViewDto, int, PagedInventaryResultRequestDto, InventaryFullDto, InventaryViewDto>, IInventaryAppService
    {
        IRepository<Inventory, int> _inventaryRepository;
        IRepository<Product, int> _productRepository;
        public InventaryAppService(IRepository<Inventory, int> repository, IRepository<Product, int> productRepository) : base(repository)
        {
            _inventaryRepository = repository;
            _productRepository = productRepository;
        }

        public int CountProductInventary(int productId)
        {
            var inventary = _inventaryRepository.FirstOrDefault(x => x.ProductId == productId);
            return inventary != null ? inventary.CurrencyQuantity : 0;
        }

        public override async Task<PagedResultDto<InventaryViewDto>> GetAllAsync(PagedInventaryResultRequestDto input)
        {
            try
            {
                CheckGetAllPermission();

                var query = CreateFilteredQuery(input);

                var totalCount = await AsyncQueryableExecuter.CountAsync(query);

                query = ApplySorting(query, input);
                query = ApplyPaging(query, input);

                var entities = await AsyncQueryableExecuter.ToListAsync(query);

                return new PagedResultDto<InventaryViewDto>(
                    totalCount,
                    entities.Select(MapToEntityDto).ToList()
                );
            }
            catch(Exception ex)
            {
                throw new UserFriendlyException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        protected override IQueryable<Inventory> CreateFilteredQuery(PagedInventaryResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Product)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Product.Name.Contains(input.Keyword))
                .WhereIf(input.WarehouseId.HasValue, x => x.WarehouseId == input.WarehouseId)
                .WhereIf(input.ProductId != null, x => x.ProductId == input.ProductId)
                ;
        }

        protected override IQueryable<Inventory> ApplySorting(IQueryable<Inventory> query, PagedInventaryResultRequestDto input)
        {
            return query.OrderBy(r => r.Id);
        }

        protected override InventaryViewDto MapToEntityDto(Inventory warehouse)
        {
            var product = _productRepository.FirstOrDefault(x => x.Id == warehouse.ProductId);
            var warehouseDto = base.MapToEntityDto(warehouse);
            warehouseDto.Product = ObjectMapper.Map<ProductViewDto>(product);
            return warehouseDto;
        }

        protected override Inventory MapToEntity(InventaryFullDto createInput)
        {
            var warehouse = ObjectMapper.Map<Inventory>(createInput);
            return warehouse;
        }

        protected override void MapToEntity(InventaryViewDto input, Inventory user)
        {
            ObjectMapper.Map(input, user);
        }

        public override async Task<InventaryViewDto> UpdateAsync(InventaryViewDto input)
        {
            try
            {
                CheckUpdatePermission();

                var entity = await GetInventaryByIdAsync(input.Id);
                ObjectMapper.Map(input, entity);
                return MapToEntityDto(entity);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        protected virtual async Task<Inventory> GetInventaryByIdAsync(int productId)
        {
            var role = await _inventaryRepository.FirstOrDefaultAsync(x => x.Id == productId);
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
