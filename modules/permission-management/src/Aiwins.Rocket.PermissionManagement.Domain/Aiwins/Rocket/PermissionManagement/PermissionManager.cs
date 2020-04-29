using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.MultiTenancy;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionManager : IPermissionManager, ISingletonDependency {
        protected IPermissionGrantRepository PermissionGrantRepository { get; }

        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }

        protected IGuidGenerator GuidGenerator { get; }

        protected ICurrentTenant CurrentTenant { get; }

        protected PermissionManagementOptions Options { get; }

        public PermissionManager (
            IPermissionDefinitionManager permissionDefinitionManager,
            IPermissionGrantRepository permissionGrantRepository,
            IServiceProvider serviceProvider,
            IGuidGenerator guidGenerator,
            IOptions<PermissionManagementOptions> options,
            ICurrentTenant currentTenant) {
            GuidGenerator = guidGenerator;
            CurrentTenant = currentTenant;
            PermissionGrantRepository = permissionGrantRepository;
            PermissionDefinitionManager = permissionDefinitionManager;
            Options = options.Value;
        }

        public virtual async Task<PermissionWithGrantedProviders> GetAsync (string permissionName, string providerName, string providerKey) {
            return await GetInternalAsync (PermissionDefinitionManager.Get (permissionName), providerName, providerKey);
        }

        public virtual async Task<List<PermissionWithGrantedProviders>> GetAllAsync (string providerName, string providerKey) {
            var results = new List<PermissionWithGrantedProviders> ();

            foreach (var permissionDefinition in PermissionDefinitionManager.GetPermissions ()) {
                results.Add (await GetInternalAsync (permissionDefinition, providerName, providerKey));
            }

            return results;
        }

        public virtual async Task SetAsync (string permissionName, string providerName, string providerScope, string providerKey) {
            var permission = PermissionDefinitionManager.Get (permissionName);

            if (permission.Providers.Any () && !permission.Providers.Contains (providerName)) {
                throw new RocketException ($"The permission named '{permission.Name}' has not compatible with the provider named '{providerName}'");
            }

            if (!permission.MultiTenancySide.HasFlag (CurrentTenant.GetMultiTenancySide ())) {
                throw new RocketException ($"The permission named '{permission.Name}' has multitenancy side '{permission.MultiTenancySide}' which is not compatible with the current multitenancy side '{CurrentTenant.GetMultiTenancySide()}'");
            }

            var currentGrantInfo = await GetInternalAsync (permission, providerName, providerKey);
            if (currentGrantInfo.Scope == providerScope) {
                return;
            }

            if (providerScope != nameof (PermissionScopeType.Prohibited)) {
                await GrantAsync (permissionName, providerName, providerKey, providerScope);
            } else {
                await RevokeAsync (permissionName, providerName, providerKey);
            }
        }

        public virtual async Task<PermissionGrant> UpdateProviderKeyAsync (PermissionGrant permissionGrant, string providerKey) {
            permissionGrant.ProviderKey = providerKey;
            return await PermissionGrantRepository.UpdateAsync (permissionGrant);
        }

        protected virtual async Task<PermissionWithGrantedProviders> GetInternalAsync (PermissionDefinition permission, string providerName, string providerKey) {
            var result = new PermissionWithGrantedProviders (permission.Name, false);

            if (!permission.MultiTenancySide.HasFlag (CurrentTenant.GetMultiTenancySide ())) {
                return result;
            }

            if (permission.Providers.Any () && !permission.Providers.Contains (providerName)) {
                return result;
            }

            var providerResult = await CheckAsync (permission.Name, providerName, providerKey);

            if (providerResult.IsGranted) {
                result.IsGranted = true;
                result.Scope = providerResult.ProviderScope;
                result.Providers.Add (new PermissionValueProviderInfo (providerName, providerResult.ProviderScope, providerResult.ProviderKey));
            }

            return result;
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

        protected virtual async Task<PermissionGrantInfo> CheckAsync (string name, string providerName, string providerKey) {

            var permissionGrant = await PermissionGrantRepository.FindAsync (name, providerName, providerKey);

            return new PermissionGrantInfo (
                permissionGrant != null,
                permissionGrant?.ProviderScope??nameof (PermissionScopeType.Prohibited),
                providerKey
            );
        }
    }
}