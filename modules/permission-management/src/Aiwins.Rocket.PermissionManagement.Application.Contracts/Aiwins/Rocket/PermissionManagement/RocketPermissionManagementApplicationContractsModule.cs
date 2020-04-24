using Aiwins.Rocket.Application;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.PermissionManagement
{
    [DependsOn(typeof(RocketDddApplicationModule))]
    [DependsOn(typeof(RocketPermissionManagementDomainSharedModule))]
    public class RocketPermissionManagementApplicationContractsModule : RocketModule
    {
        
    }
}
