using System;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Identity {
    public class UserRoleFinder : IUserRoleFinder, ITransientDependency {
        protected IIdentityUserRepository IdentityUserRepository { get; }

        public UserRoleFinder (IIdentityUserRepository identityUserRepository) {
            IdentityUserRepository = identityUserRepository;
        }

        public virtual async Task<string[]> GetRolesAsync (Guid userId) {
            return (await IdentityUserRepository.GetRoleNamesAsync (userId)).ToArray ();
        }
    }
}