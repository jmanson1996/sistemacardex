using AutoMapper;

namespace Factora.Inventories.Dto
{
    public class InventaryProfileMapper : Profile
    {
        public InventaryProfileMapper()
        {
            CreateMap<Inventory, InventaryViewDto>();
            CreateMap<Inventory, InventaryFullDto>();
            CreateMap<InventaryFullDto, Inventory>();
            CreateMap<InventaryViewDto, Inventory>();
        }
    }
}
