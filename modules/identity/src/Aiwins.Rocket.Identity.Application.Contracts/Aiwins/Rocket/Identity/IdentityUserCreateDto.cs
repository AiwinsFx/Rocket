using System.ComponentModel.DataAnnotations;
using Aiwins.Rocket.Auditing;

namespace Aiwins.Rocket.Identity {
    public class IdentityUserCreateDto : IdentityUserCreateOrUpdateDtoBase {
        [Required]
        [StringLength (IdentityUserConsts.MaxPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }
    }
}