using Aiwins.Rocket.Application;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.TenantManagement
{
    [DependsOn(
        typeof(RocketDddApplicationModule),
        typeof(RocketTenantManagementDomainSharedModule))]
    public class RocketTenantManagementApplicationContractsModule : RocketModule
    {

    }
}