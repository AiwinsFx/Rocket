using System.ComponentModel.DataAnnotations;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Domain.Entities;

namespace Aiwins.Rocket.Identity
{
    public class IdentityUserUpdateDto : IdentityUserCreateOrUpdateDtoBase, IHasConcurrencyStamp
    {
        [StringLength(IdentityUserConsts.MaxPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }
        
        public string ConcurrencyStamp { get; set; }
    }
}