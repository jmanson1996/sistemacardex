using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.UI;
using Factora.OrdersDetails.Dto;
using Factora.Products.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Factora.OrdersDetails
{
    public class OrderDetailAppService : AsyncCrudAppService<OrderDetail,OrderDetailViewDto, int, PagedOrderDetailResultRequestDto, OrderDetailFullDto, OrderDetailViewDto>, IOrderDetailAppService
    {
        private readonly IRepository<OrderDetail, int> _repository;
        public OrderDetailAppService(IRepository<OrderDetail, int> repository) : base(repository)
        {
            _repository = repository;
        }
        protected override IQueryable<OrderDetail> CreateFilteredQuery(PagedOrderDetailResultRequestDto input)
        {
            return Repository.GetAllIncluding()
            .WhereIf(input.OrderDetail != null, x => x.OrderId == input.OrderDetail);
        }

        protected override IQueryable<OrderDetail> ApplySorting(IQueryable<OrderDetail> query, PagedOrderDetailResultRequestDto input)
        {
            return query.OrderBy(r => r.Id);
        }

        protected override OrderDetailViewDto MapToEntityDto(OrderDetail warehouse)
        {
            var warehouseDto = base.MapToEntityDto(warehouse);
            return warehouseDto;
        }

        protected override OrderDetail MapToEntity(OrderDetailFullDto createInput)
        {
            var warehouse = ObjectMapper.Map<OrderDetail>(createInput);
            return warehouse;
        }

        protected override void MapToEntity(OrderDetailViewDto input, OrderDetail user)
        {
            ObjectMapper.Map(input, user);
        }

        public override async Task<OrderDetailViewDto> UpdateAsync(OrderDetailViewDto input)
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

        protected virtual async Task<OrderDetail> GetOrderByIdAsync(int productId)
        {
            var role = await _repository.FirstOrDefaultAsync(x => x.Id == productId);
            if (role == null)
            {
                throw new AbpException("There is no Detail with id: " + productId);
            }

            return role;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
