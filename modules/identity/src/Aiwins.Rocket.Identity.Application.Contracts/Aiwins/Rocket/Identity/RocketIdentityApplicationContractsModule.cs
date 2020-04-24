using Aiwins.Rocket.Application;
using Aiwins.Rocket.Authorization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement;
using Aiwins.Rocket.Users;

namespace Aiwins.Rocket.Identity
{
    [DependsOn(
        typeof(RocketIdentityDomainSharedModule),
        typeof(RocketUsersAbstractionModule),
        typeof(RocketAuthorizationModule),
        typeof(RocketDddApplicationModule),
        typeof(RocketPermissionManagementApplicationContractsModule)
        )]
    public class RocketIdentityApplicationContractsModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}