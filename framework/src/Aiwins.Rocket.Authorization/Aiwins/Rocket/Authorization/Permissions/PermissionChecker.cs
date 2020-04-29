using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.Security.Claims;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class PermissionChecker : IPermissionChecker, ITransientDependency {
        protected RocketPermissionOptions Options { get; }
        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }
        protected ICurrentPrincipalAccessor PrincipalAccessor { get; }
        protected ICurrentTenant CurrentTenant { get; }
        protected IPermissionValueProviderManager PermissionValueProviderManager { get; }

        public PermissionChecker (
            IOptions<RocketPermissionOptions> options,
            ICurrentPrincipalAccessor principalAccessor,
            IPermissionDefinitionManager permissionDefinitionManager,
            ICurrentTenant currentTenant,
            IPermissionValueProviderManager permissionValueProviderManager) {
            Options = options.Value;
            PrincipalAccessor = principalAccessor;
            PermissionDefinitionManager = permissionDefinitionManager;
            CurrentTenant = currentTenant;
            PermissionValueProviderManager = permissionValueProviderManager;
        }

        public virtual async Task<PermissionGrantResult> GetResultAsync (string name) {
            return await GetResultAsync (PrincipalAccessor.Principal, name);
        }

        public virtual async Task<PermissionGrantResult> GetResultAsync (
            ClaimsPrincipal claimsPrincipal,
            string name) {
            Check.NotNull (name, nameof (name));

            var permission = PermissionDefinitionManager.Get (name);

            if (!permission.IsEnabled) {
                return PermissionGrantResult.Prohibited;
            }

            var multiTenancySide = claimsPrincipal?.GetMultiTenancySide () ??
                CurrentTenant.GetMultiTenancySide ();

            if (!permission.MultiTenancySide.HasFlag (multiTenancySide)) {
                return PermissionGrantResult.Prohibited;
            }

            var context = new PermissionValueCheckContext (permission, claimsPrincipal);

            // 可选权限策略:一、获取最大权限；二、以用户权限为主
            // 当前权限策略:以用户权限为主（用户权限 > 角色权限 > 客户端权限）
            // 对权限提供程序排序，用户权限放到最后赋值，覆盖前者权限
            var providers = PermissionValueProviderManager.ValueProviders.OrderBy (m => m.Name).ToList ();

            var permissionGrantResult = PermissionGrantResult.Undefined;
            foreach (var provider in providers) {
                if (context.Permission.Providers.Any () && !context.Permission.Providers.Contains (provider.Name)) {
                    continue;
                }

                var result = await provider.GetResultAsync (context);

                // 以用户权限为主，将用户权限解析程序放到最后赋值，覆盖前者权限（用户权限 > 角色权限 > 客户端权限）
                if (Options.PermissionPolicy == PermissionPolicy.User) {
                    permissionGrantResult = result;
                } else {
                    // 以获取的最大权限为主
                    if (result?.GrantType == PermissionGrantType.Granted && result?.ScopeType > permissionGrantResult.ScopeType) {
                        permissionGrantResult = result;
                    }
                }
            }

            return permissionGrantResult;
        }
    }
}