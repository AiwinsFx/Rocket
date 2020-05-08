using Aiwins.Rocket.Domain.Entities;

namespace Aiwins.Rocket.Identity {
    public class IdentityRoleUpdateDto : IdentityRoleCreateOrUpdateDtoBase, IHasConcurrencyStamp {
        public string ConcurrencyStamp { get; set; }
    }
}