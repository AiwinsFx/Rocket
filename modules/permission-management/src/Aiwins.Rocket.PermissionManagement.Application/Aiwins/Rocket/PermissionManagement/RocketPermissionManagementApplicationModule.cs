using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.PermissionManagement {
    [DependsOn (
        typeof (RocketPermissionManagementDomainModule),
        typeof (RocketPermissionManagementApplicationContractsModule)
    )]
    public class RocketPermissionManagementApplicationModule : RocketModule {

    }
}