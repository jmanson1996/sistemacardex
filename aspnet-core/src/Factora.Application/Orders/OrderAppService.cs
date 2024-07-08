using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.UI;
using Factora.Orders.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Factora.Orders
{
    public class OrderAppService : AsyncCrudAppService<Order,OrderViewDto, int, PagedOrderResultRequestDto, OrderFullDto, OrderViewDto>, IOrderAppService
    {
        private readonly IRepository<Order, int> _repository;
        public OrderAppService(IRepository<Order, int> repository) : base(repository)
        {
            _repository = repository;
        }
        
        protected override IQueryable<Order> CreateFilteredQuery(PagedOrderResultRequestDto input)
        {
            return Repository.GetAllIncluding();
                //.WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
                //|| x.Description.Contains(input.Keyword));
        }

        protected override IQueryable<Order> ApplySorting(IQueryable<Order> query, PagedOrderResultRequestDto input)
        {
            return query.OrderBy(r => r.Id);
        }

        protected override OrderViewDto MapToEntityDto(Order warehouse)
        {
            var warehouseDto = base.MapToEntityDto(warehouse);
            return warehouseDto;
        }

        protected override Order MapToEntity(OrderFullDto createInput)
        {
            var warehouse = ObjectMapper.Map<Order>(createInput);
            return warehouse;
        }

        protected override void MapToEntity(OrderViewDto input, Order user)
        {
            ObjectMapper.Map(input, user);
        }

        public override async Task<OrderViewDto> UpdateAsync(OrderViewDto input)
        {
            try
            {
                CheckUpdatePermission();

                var entity = await GetOrderByIdAsync(input.Id);
                ObjectMapper.Map(input, entity);
                return MapToEntityDto(entity);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        protected virtual async Task<Order> GetOrderByIdAsync(int productId)
        {
            var role = await _repository.FirstOrDefaultAsync(x => x.Id == productId);
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
