using AutoMapper;
using Factora.Users.Dto;

namespace Factora.Warehouses.Dto
{
    public class WarehouseProfileMapper : Profile
    {
        public WarehouseProfileMapper()
        {
            CreateMap<Warehouse, WarehouseViewDto>();
            CreateMap<WarehouseViewDto, Warehouse>();
            CreateMap<Warehouse, WarehouseFullDto>();
            CreateMap<WarehouseFullDto, Warehouse>();
        }
    }
}
