using Aiwins.Rocket.Account;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement;
using Aiwins.Rocket.TenantManagement;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameDomainModule),
        typeof(RocketAccountApplicationModule),
        typeof(MyProjectNameApplicationContractsModule),
        typeof(RocketIdentityApplicationModule),
        typeof(RocketPermissionManagementApplicationModule),
        typeof(RocketTenantManagementApplicationModule),
        typeof(RocketFeatureManagementApplicationModule)
        )]
    public class MyProjectNameApplicationModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddMaps<MyProjectNameApplicationModule>();
            });
        }
    }
}
