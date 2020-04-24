using System;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Rocket.Domain.Entities;

namespace Aiwins.Rocket.Identity
{
    public class IdentityRoleDto : ExtensibleEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; }

        public bool IsDefault { get; set; }
        
        public bool IsStatic { get; set; }

        public bool IsPublic { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}