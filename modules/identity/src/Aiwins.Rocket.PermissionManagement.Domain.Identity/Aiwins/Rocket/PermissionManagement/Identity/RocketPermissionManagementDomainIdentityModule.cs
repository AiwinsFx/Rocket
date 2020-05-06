using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.PermissionManagement.Identity {
    [DependsOn (
        typeof (RocketIdentityDomainSharedModule),
        typeof (RocketPermissionManagementDomainModule)
    )]
    public class RocketPermissionManagementDomainIdentityModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<PermissionManagementOptions> (options => {
                options.ManagementProviders.Add<UserPermissionManagementProvider> ();
                options.ManagementProviders.Add<RolePermissionManagementProvider> ();

                //TODO: Can we prevent duplication of permission names without breaking the design and making the system complicated
                options.ProviderPolicies[UserPermissionValueProvider.ProviderName] = "RocketIdentity.Users.ManagePermissions";
                options.ProviderPolicies[RolePermissionValueProvider.ProviderName] = "RocketIdentity.Roles.ManagePermissions";
            });
        }
    }
}