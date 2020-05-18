using System.ComponentModel.DataAnnotations;
using Aiwins.Rocket.ObjectExtending;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Identity {
    public abstract class IdentityUserCreateOrUpdateDtoBase : ExtensibleObject {
        [Required]
        [StringLength (IdentityUserConsts.MaxUserNameLength)]
        public string UserName { get; set; }

        [StringLength (IdentityUserConsts.MaxNameLength)]
        public string Name { get; set; }

        [StringLength (IdentityUserConsts.MaxSurnameLength)]
        public string Surname { get; set; }

        [EmailAddress]
        [StringLength (IdentityUserConsts.MaxEmailLength)]
        public string Email { get; set; }

        [Required]
        [RegularExpression (@"^1[3456789][0-9]{9}$")]
        [StringLength (IdentityUserConsts.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool LockoutEnabled { get; set; }

        [CanBeNull]
        public string[] RoleNames { get; set; }
    }
}