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

        public override async Task<PermissionGrantInfo> CheckAsync (string name, string providerName, string providerKey) {
            if (providerName == Name) {
                var permissionGrant = await PermissionGrantRepository.FindAsync (name, providerName, providerKey);

                return new PermissionGrantInfo (
                    permissionGrant != null,
                    permissionGrant?.ProviderScope??nameof (PermissionScopeType.Prohibited),
                    providerKey
                );
            }

            var permissionGrantResult = PermissionGrantInfo.NonGranted;

            if (providerName == UserPermissionValueProvider.ProviderName) {
                var userId = Guid.Parse (providerKey);
                var roles = await UserRoleFinder.GetRolesAsync (userId);

                foreach (var role in roles) {
                    var result = await PermissionGrantRepository.FindAsync (name, Name, role);
                    if (result == null) continue;

                    // 以角色的最大权限为主
                    var permissionGrantScope = PermissionScopeType.Prohibited;
                    if (Enum.TryParse (permissionGrantResult.ProviderScope, out PermissionScopeType ps)) permissionGrantScope = ps;

                    var resultScope = PermissionScopeType.Prohibited;
                    if (Enum.TryParse (result.ProviderScope, out PermissionScopeType rs)) resultScope = rs;

                    if (resultScope > permissionGrantScope) {
                        permissionGrantResult = new PermissionGrantInfo (true, result.ProviderScope, role);
                    }
                }
            }

            return permissionGrantResult;
        }
    }
}