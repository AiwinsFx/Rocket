using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Identity.Localization;
using Aiwins.Rocket.Localization;

namespace Aiwins.Rocket.Identity {
    public class IdentityPermissionDefinitionProvider : PermissionDefinitionProvider {
        public override void Define (IPermissionDefinitionContext context) {
            var identityGroup = context.AddGroup (IdentityPermissions.GroupName, L ("Permission:IdentityManagement"));

            var rolesPermission = identityGroup.AddPermission (IdentityPermissions.Roles.Default, L ("Permission:RoleManagement")).WithScopes (Prohibited, Platform, All, Domain, Owner);
            rolesPermission.AddChild (IdentityPermissions.Roles.Create, L ("Permission:Create")).WithScopes (Prohibited, Granted);
            rolesPermission.AddChild (IdentityPermissions.Roles.Update, L ("Permission:Edit")).WithScopes (Prohibited, Granted);
            rolesPermission.AddChild (IdentityPermissions.Roles.Delete, L ("Permission:Delete")).WithScopes (Prohibited, Granted);
            rolesPermission.AddChild (IdentityPermissions.Roles.ManagePermissions, L ("Permission:ChangePermissions")).WithScopes (Prohibited, Granted);

            var usersPermission = identityGroup.AddPermission (IdentityPermissions.Users.Default, L ("Permission:UserManagement"));
            usersPermission.AddChild (IdentityPermissions.Users.Create, L ("Permission:Create")).WithScopes (Prohibited, Granted);
            usersPermission.AddChild (IdentityPermissions.Users.Update, L ("Permission:Edit")).WithScopes (Prohibited, Granted);
            usersPermission.AddChild (IdentityPermissions.Users.Delete, L ("Permission:Delete")).WithScopes (Prohibited, Granted);
            usersPermission.AddChild (IdentityPermissions.Users.ManagePermissions, L ("Permission:ChangePermissions")).WithScopes (Prohibited, Granted);

            identityGroup
                .AddPermission (IdentityPermissions.UserLookup.Default, L ("Permission:UserLookup"))
                .WithScopes (Prohibited, Platform, All, Domain, Owner)
                .WithProviders (ClientPermissionValueProvider.ProviderName);
        }

        private static LocalizableString L (string name) {
            return LocalizableString.Create<IdentityResource> (name);
        }
    }
}