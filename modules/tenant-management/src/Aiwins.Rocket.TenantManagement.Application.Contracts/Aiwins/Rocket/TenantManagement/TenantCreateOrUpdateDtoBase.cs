using System.ComponentModel.DataAnnotations;
using Aiwins.Rocket.ObjectExtending;

namespace Aiwins.Rocket.TenantManagement
{
    public abstract class TenantCreateOrUpdateDtoBase : ExtensibleObject
    {
        [Required]
        [StringLength(TenantConsts.MaxNameLength)]
        public string Name { get; set; }
    }
}