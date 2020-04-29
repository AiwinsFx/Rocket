using System;
using Aiwins.Rocket.Application.Dtos;

namespace Aiwins.Rocket.TenantManagement {
    public class TenantDto : ExtensibleEntityDto<Guid> {
        public string Name { get; set; }
    }
}