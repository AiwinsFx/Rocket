using Aiwins.Rocket.Account;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.ObjectExtending;
using Aiwins.Rocket.PermissionManagement;
using Aiwins.Rocket.TenantManagement;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameDomainSharedModule),
        typeof(RocketAccountApplicationContractsModule),
        typeof(RocketFeatureManagementApplicationContractsModule),
        typeof(RocketIdentityApplicationContractsModule),
        typeof(RocketPermissionManagementApplicationContractsModule),
        typeof(RocketTenantManagementApplicationContractsModule),
        typeof(RocketObjectExtendingModule)
    )]
    public class MyProjectNameApplicationContractsModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            MyProjectNameDtoExtensions.Configure();
        }
    }
}
