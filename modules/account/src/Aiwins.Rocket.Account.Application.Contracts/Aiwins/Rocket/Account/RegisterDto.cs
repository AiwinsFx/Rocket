using System.ComponentModel.DataAnnotations;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Identity;

namespace Aiwins.Rocket.Account {
    public class RegisterDto {
        [Required]
        [StringLength (IdentityUserConsts.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [RegularExpression (@"^1[3456789][0-9]{9}$")]
        [StringLength (IdentityUserConsts.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength (IdentityUserConsts.MaxPasswordLength)]
        [DataType (DataType.Password)]
        [DisableAuditing]
        public string Password { get; set; }

        [Required]
        public string AppName { get; set; }
    }
}