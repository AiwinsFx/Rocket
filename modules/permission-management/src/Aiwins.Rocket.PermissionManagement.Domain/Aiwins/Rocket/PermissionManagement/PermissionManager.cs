using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionManager : IPermissionManager, ISingletonDependency {
        protected RocketPermissionOptions PermissionOptions { get; }
        protected IPermissionGrantRepository PermissionGrantRepository { get; }

        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }

        protected IGuidGenerator GuidGenerator { get; }

        protected ICurrentTenant CurrentTenant { get; }

        protected IReadOnlyList<IPermissionManagementProvider> Providers => _lazyProviders.Value.OrderByDescending (m => m.Name).ToList ();

        protected PermissionManagementOptions Options { get; }

        private readonly Lazy<List<IPermissionManagementProvider>> _lazyProviders;

        public PermissionManager (
            IPermissionDefinitionManager permissionDefinitionManager,
            IPermissionGrantRepository permissionGrantRepository,
            IServiceProvider serviceProvider,
            IGuidGenerator guidGenerator,
            IOptions<RocketPermissionOptions> permissionOptions,
            IOptions<PermissionManagementOptions> options,
            ICurrentTenant currentTenant) {
            GuidGenerator = guidGenerator;
            CurrentTenant = currentTenant;
            PermissionGrantRepository = permissionGrantRepository;
            PermissionDefinitionManager = permissionDefinitionManager;
            PermissionOptions = permissionOptions.Value;
            Options = options.Value;

            _lazyProviders = new Lazy<List<IPermissionManagementProvider>> (
                () => Options
                .Providers
                .Select (c => serviceProvider.GetRequiredService (c) as IPermissionManagementProvider)
                .ToList (),
                true
            );
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

            var provider = Providers.FirstOrDefault (m => m.Name == providerName);
            if (provider == null) {
                throw new RocketException ("Unknown permission management provider: " + providerName);
            }

            await provider.SetAsync (permissionName, providerKey, providerScope);
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

            // 可选权限策略:一、获取最大权限；二、以用户权限为主
            // 当前权限策略:以用户权限为主（用户权限 > 角色权限 > 客户端权限）
            // 对权限提供程序排序，用户权限放到最后赋值，覆盖前者权限
            foreach (var provider in Providers) {
                var providerResult = await provider.CheckAsync (permission.Name, providerName, providerKey);

                if (providerResult.IsGranted) {
                    result.IsGranted = true;
                    // 以用户权限为主，将用户权限解析程序放到最后赋值，覆盖前者权限（用户权限 > 角色权限 > 客户端权限）
                    if (PermissionOptions.PermissionPolicy == PermissionPolicy.User) {
                        result.Scope = providerResult.ProviderScope;
                    } else {
                        // 以最大权限为主
                        var providerScope = PermissionScopeType.Prohibited;
                        if (Enum.TryParse (providerResult.ProviderScope, out PermissionScopeType ps)) providerScope = ps;
                        
                        var resultScope = PermissionScopeType.Prohibited;
                        if (Enum.TryParse (result.Scope, out PermissionScopeType rs)) resultScope = rs;

                        if (providerScope > resultScope) {
                            result.Scope = providerResult.ProviderScope;
                        }
                    }

                    result.Providers.Add (new PermissionValueProviderInfo (provider.Name, providerResult.ProviderScope, providerResult.ProviderKey));
                }
            }

            return result;
        }
    }
}