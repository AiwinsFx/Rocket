using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.TenantManagement.Localization;

namespace Aiwins.Rocket.TenantManagement {
    public class RocketTenantManagementPermissionDefinitionProvider : PermissionDefinitionProvider {
        public override void Define (IPermissionDefinitionContext context) {
            var tenantManagementGroup = context.AddGroup (TenantManagementPermissions.GroupName, L ("Permission:TenantManagement"));

            var tenantsPermission = tenantManagementGroup.AddPermission (TenantManagementPermissions.Tenants.Default, L ("Permission:TenantManagement"), multiTenancySide : MultiTenancySides.Host).WithScopes (Granted, Prohibited);
            
            tenantsPermission.AddChild (TenantManagementPermissions.Tenants.Create, L ("Permission:Create"), multiTenancySide : MultiTenancySides.Host).WithScopes (Granted, Prohibited);
            tenantsPermission.AddChild (TenantManagementPermissions.Tenants.Update, L ("Permission:Edit"), multiTenancySide : MultiTenancySides.Host).WithScopes (Granted, Prohibited);
            tenantsPermission.AddChild (TenantManagementPermissions.Tenants.Delete, L ("Permission:Delete"), multiTenancySide : MultiTenancySides.Host).WithScopes (Granted, Prohibited);
            tenantsPermission.AddChild (TenantManagementPermissions.Tenants.ManageFeatures, L ("Permission:ManageFeatures"), multiTenancySide : MultiTenancySides.Host).WithScopes (Granted, Prohibited);
            tenantsPermission.AddChild (TenantManagementPermissions.Tenants.ManageConnectionStrings, L ("Permission:ManageConnectionStrings"), multiTenancySide : MultiTenancySides.Host).WithScopes (Granted, Prohibited);
        }

        private static LocalizableString L (string name) {
            return LocalizableString.Create<RocketTenantManagementResource> (name);
        }
    }
}