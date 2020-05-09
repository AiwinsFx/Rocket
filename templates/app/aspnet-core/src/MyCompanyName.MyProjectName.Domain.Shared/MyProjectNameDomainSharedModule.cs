using MyCompanyName.MyProjectName.Localization;
using Aiwins.Rocket.AuditLogging;
using Aiwins.Rocket.BackgroundJobs;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.IdentityServer;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement;
using Aiwins.Rocket.SettingManagement;
using Aiwins.Rocket.TenantManagement;
using Aiwins.Rocket.Validation.Localization;
using Aiwins.Rocket.VirtualFileSystem;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(RocketAuditLoggingDomainSharedModule),
        typeof(RocketBackgroundJobsDomainSharedModule),
        typeof(RocketFeatureManagementDomainSharedModule),
        typeof(RocketIdentityDomainSharedModule),
        typeof(RocketIdentityServerDomainSharedModule),
        typeof(RocketPermissionManagementDomainSharedModule),
        typeof(RocketSettingManagementDomainSharedModule),
        typeof(RocketTenantManagementDomainSharedModule)
        )]
    public class MyProjectNameDomainSharedModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<MyProjectNameDomainSharedModule>("MyCompanyName.MyProjectName");
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<MyProjectNameResource>("en")
                    .AddBaseTypes(typeof(RocketValidationResource))
                    .AddVirtualJson("/Localization/MyProjectName");
                
                options.DefaultResourceType = typeof(MyProjectNameResource);
            });
        }
    }
}
