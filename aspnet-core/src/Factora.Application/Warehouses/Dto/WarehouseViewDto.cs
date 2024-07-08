using Abp.Application.Services.Dto;
using Factora.Users.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace Factora.Warehouses.Dto
{
    public class WarehouseViewDto : FullAuditedEntityDto<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Location { get; set; }
        public string Phone { get; set; }
        public long UserId { get; set; }
        public UserDto User { get; set; }
        public TypeWarehouse Type { get; set; }
    }
}
