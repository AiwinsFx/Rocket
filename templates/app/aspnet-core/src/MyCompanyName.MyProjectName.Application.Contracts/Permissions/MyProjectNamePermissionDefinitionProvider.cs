using MyCompanyName.MyProjectName.Localization;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Localization;

namespace MyCompanyName.MyProjectName.Permissions
{
    public class MyProjectNamePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(MyProjectNamePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(MyProjectNamePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MyProjectNameResource>(name);
        }
    }
}
