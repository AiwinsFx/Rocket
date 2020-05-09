using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Localization;
using Aiwins.Docs.Localization;

namespace Aiwins.Docs.Admin
{
    public class DocsAdminPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(DocsAdminPermissions.GroupName, L("Permission:DocumentManagement"));

            var projects = group.AddPermission(DocsAdminPermissions.Projects.Default, L("Permission:Projects"));
            projects.AddChild(DocsAdminPermissions.Projects.Update, L("Permission:Edit"));
            projects.AddChild(DocsAdminPermissions.Projects.Delete, L("Permission:Delete"));
            projects.AddChild(DocsAdminPermissions.Projects.Create, L("Permission:Create"));

            group.AddPermission(DocsAdminPermissions.Documents.Default, L("Permission:Documents"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DocsResource>(name);
        }
    }
}
