using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionDataSeedContributor : IDataSeedContributor, ITransientDependency {
        protected ICurrentTenant CurrentTenant { get; }

        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }
        protected IPermissionDataSeeder PermissionDataSeeder { get; }

        public PermissionDataSeedContributor (
            IPermissionDefinitionManager permissionDefinitionManager,
            IPermissionDataSeeder permissionDataSeeder,
            ICurrentTenant currentTenant) {
            PermissionDefinitionManager = permissionDefinitionManager;
            PermissionDataSeeder = permissionDataSeeder;
            CurrentTenant = currentTenant;
        }

        public virtual Task SeedAsync (DataSeedContext context) {
            var multiTenancySide = CurrentTenant.GetMultiTenancySide ();
            var permissionDefinitions = PermissionDefinitionManager
                .GetPermissions ()
                .Where (p => p.MultiTenancySide.HasFlag (multiTenancySide))
                .ToArray ();

            var permissions = new Dictionary<string, string> ();
            foreach (var permissionDefinition in permissionDefinitions) {
                permissions.Add (permissionDefinition.Name, permissionDefinition.Scopes.FirstOrDefault ().Name);
            }

            return PermissionDataSeeder.SeedAsync (
                RolePermissionValueProvider.ProviderName,
                "超级管理员",
                permissions,
                context.TenantId
            );
        }
    }
}