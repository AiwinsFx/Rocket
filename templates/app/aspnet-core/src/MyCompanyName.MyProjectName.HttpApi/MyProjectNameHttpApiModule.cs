using Aiwins.Rocket.Account;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement.HttpApi;
using Aiwins.Rocket.TenantManagement;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameApplicationContractsModule),
        typeof(RocketAccountHttpApiModule),
        typeof(RocketIdentityHttpApiModule),
        typeof(RocketPermissionManagementHttpApiModule),
        typeof(RocketTenantManagementHttpApiModule),
        typeof(RocketFeatureManagementHttpApiModule)
        )]
    public class MyProjectNameHttpApiModule : RocketModule
    {
        
    }
}
