using System.ComponentModel.DataAnnotations;
using Aiwins.Rocket.ObjectExtending;

namespace Aiwins.Rocket.Identity
{
    public class IdentityRoleCreateOrUpdateDtoBase : ExtensibleObject
    {
        [Required]
        [StringLength(IdentityRoleConsts.MaxNameLength)]
        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public bool IsPublic { get; set; }
    }
}