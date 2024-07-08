using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.UI;
using Factora.Authorization.Users;
using Factora.Users.Dto;
using Factora.Warehouses.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Factora.Warehouses
{
    public class WarehouseAppService : AsyncCrudAppService<Warehouse,WarehouseViewDto, Guid, PagedWarehouseResultRequestDto, WarehouseFullDto, WarehouseViewDto>, IWarehouseAppService
    {
        private readonly IRepository<Warehouse, Guid> _warehouseRepository;
        private readonly IRepository<User, long> _userRepository;
        public WarehouseAppService(IRepository<Warehouse, Guid> warehouseRepository, 
                                   IRepository<User, long> userRepository) : base(warehouseRepository)
        {
            _userRepository = userRepository;
            _warehouseRepository = warehouseRepository;
        }

        protected override IQueryable<Warehouse> CreateFilteredQuery(PagedWarehouseResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.User)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
                || x.Phone.Contains(input.Keyword)
                || x.Location.Contains(input.Keyword));
        }

        protected override IQueryable<Warehouse> ApplySorting(IQueryable<Warehouse> query, PagedWarehouseResultRequestDto input)
        {
            return query.OrderBy(r => r.Id);
        }

        protected override WarehouseViewDto MapToEntityDto(Warehouse warehouse)
        {
            var user = _userRepository.FirstOrDefault(warehouse.UserId);
            var warehouseDto = base.MapToEntityDto(warehouse);
            warehouseDto.User = ObjectMapper.Map<UserDto>(user);
            return warehouseDto;
        }

        protected override Warehouse MapToEntity(WarehouseFullDto createInput)
        {
            var warehouse = ObjectMapper.Map<Warehouse>(createInput);
            return warehouse;
        }

        protected override void MapToEntity(WarehouseViewDto input, Warehouse user)
        {
            ObjectMapper.Map(input, user);
        }

        public override async Task<WarehouseViewDto> UpdateAsync(WarehouseViewDto input)
        {
            try
            {
                CheckUpdatePermission();

                var entity = await GetWarehouseByIdAsync(input.Id);
                ObjectMapper.Map(input, entity);
                return MapToEntityDto(entity);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        protected virtual async Task<Warehouse> GetWarehouseByIdAsync(Guid warehouseId)
        {
            var role = await _warehouseRepository.FirstOrDefaultAsync(warehouseId);
            if (role == null)
            {
                throw new AbpException("There is no warehouse with id: " + warehouseId);
            }

            return role;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
