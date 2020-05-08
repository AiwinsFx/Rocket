using System.ComponentModel.DataAnnotations;

namespace Aiwins.Rocket.Identity {
    public class IdentityUserUpdateRolesDto {
        [Required]
        public string[] RoleNames { get; set; }
    }
}