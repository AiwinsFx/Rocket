using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.PermissionManagement {
    public abstract class PermissionManagementProvider : IPermissionManagementProvider {
        public abstract string Name { get; }

        protected IPermissionGrantRepository PermissionGrantRepository { get; }

        protected IGuidGenerator GuidGenerator { get; }

        protected ICurrentTenant CurrentTenant { get; }

        protected PermissionManagementProvider (
            IPermissionGrantRepository permissionGrantRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant) {
            PermissionGrantRepository = permissionGrantRepository;
            GuidGenerator = guidGenerator;
            CurrentTenant = currentTenant;
        }

        public virtual async Task<PermissionGrantInfo> CheckAsync (string name, string providerName, string providerKey) {
            if (providerName != Name) {
                return PermissionGrantInfo.NonGranted;
            }

            var permissionGrant = await PermissionGrantRepository.FindAsync (name, providerName, providerKey);

            return new PermissionGrantInfo (
                permissionGrant != null,
                permissionGrant?.ProviderScope??nameof (PermissionScopeType.Prohibited),
                providerKey
            );
        }

        public virtual Task SetAsync (string name, string providerKey, string providerScope) {
            return providerScope != nameof (PermissionScopeType.Prohibited) ?
                GrantAsync (name, Name, providerScope, providerKey) :
                RevokeAsync (name, Name, providerKey);
        }

        protected virtual async Task GrantAsync (string permissionName, string providerName, string providerScope, string providerKey) {
            var permissionGrant = await PermissionGrantRepository.FindAsync (permissionName, providerName, providerKey);
            if (permissionGrant != null) {
                if (permissionGrant.ProviderScope == providerScope)
                    return;
                permissionGrant.ProviderScope = providerScope;
                await PermissionGrantRepository.UpdateAsync (permissionGrant);
            }

            await PermissionGrantRepository.InsertAsync (
                new PermissionGrant (
                    GuidGenerator.Create (),
                    permissionName,
                    providerName,
                    providerScope,
                    providerKey,
                    CurrentTenant.Id
                )
            );
        }

        protected virtual async Task RevokeAsync (string permissionName, string providerName, string providerKey) {
            var permissionGrant = await PermissionGrantRepository.FindAsync (permissionName, providerName, providerKey);
            if (permissionGrant == null) {
                return;
            }

            await PermissionGrantRepository.DeleteAsync (permissionGrant);
        }
    }
}