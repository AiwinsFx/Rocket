using MyCompanyName.MyProjectName.MultiTenancy;
using MyCompanyName.MyProjectName.ObjectExtending;
using Aiwins.Rocket.AuditLogging;
using Aiwins.Rocket.BackgroundJobs;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.IdentityServer;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.PermissionManagement.Identity;
using Aiwins.Rocket.PermissionManagement.IdentityServer;
using Aiwins.Rocket.SettingManagement;
using Aiwins.Rocket.TenantManagement;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameDomainSharedModule),
        typeof(RocketAuditLoggingDomainModule),
        typeof(RocketBackgroundJobsDomainModule),
        typeof(RocketFeatureManagementDomainModule),
        typeof(RocketIdentityDomainModule),
        typeof(RocketPermissionManagementDomainIdentityModule),
        typeof(RocketIdentityServerDomainModule),
        typeof(RocketPermissionManagementDomainIdentityServerModule),
        typeof(RocketSettingManagementDomainModule),
        typeof(RocketTenantManagementDomainModule)
        )]
    public class MyProjectNameDomainModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            MyProjectNameDomainObjectExtensions.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
        }
    }
}
