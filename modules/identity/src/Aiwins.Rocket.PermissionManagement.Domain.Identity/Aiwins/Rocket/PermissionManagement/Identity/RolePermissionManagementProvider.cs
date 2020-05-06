using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.PermissionManagement.Identity {
    public class RolePermissionManagementProvider : PermissionManagementProvider {
        public override string Name => RolePermissionValueProvider.ProviderName;

        protected IUserRoleFinder UserRoleFinder { get; }

        public RolePermissionManagementProvider (
            IPermissionGrantRepository permissionGrantRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IUserRoleFinder userRoleFinder) : base (
            permissionGrantRepository,
            guidGenerator,
            currentTenant) {
            UserRoleFinder = userRoleFinder;
        }

        public override async Task<PermissionValueProviderGrantInfo> CheckAsync (string name, string providerName, string providerKey) {
            if (providerName == Name) {
                return new PermissionValueProviderGrantInfo (
                    await PermissionGrantRepository.FindAsync (name, providerName, providerKey) != null,
                    providerKey
                );
            }

            if (providerName == UserPermissionValueProvider.ProviderName) {
                var userId = Guid.Parse (providerKey);
                var roleNames = await UserRoleFinder.GetRolesAsync (userId);

                foreach (var roleName in roleNames) {
                    var permissionGrant = await PermissionGrantRepository.FindAsync (name, Name, roleName);
                    if (permissionGrant != null) {
                        return new PermissionValueProviderGrantInfo (true, roleName);
                    }
                }
            }

            return PermissionValueProviderGrantInfo.NonGranted;
        }
    }
}