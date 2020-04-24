using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.IdentityServer;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.PermissionManagement.IdentityServer
{
    [DependsOn(
        typeof(RocketIdentityServerDomainSharedModule),
        typeof(RocketPermissionManagementDomainModule)
    )]
    public class RocketPermissionManagementDomainIdentityServerModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PermissionManagementOptions>(options =>
            {
                options.ManagementProviders.Add<ClientPermissionManagementProvider>();

                options.ProviderPolicies[ClientPermissionValueProvider.ProviderName] = "IdentityServer.Client.ManagePermissions";
            });
        }
    }
}
