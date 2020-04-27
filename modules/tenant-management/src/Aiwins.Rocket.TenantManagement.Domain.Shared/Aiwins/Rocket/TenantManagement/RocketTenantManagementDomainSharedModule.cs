using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.TenantManagement.Localization;
using Aiwins.Rocket.Validation;
using Aiwins.Rocket.Validation.Localization;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.TenantManagement {
    [DependsOn (typeof (RocketValidationModule))]
    public class RocketTenantManagementDomainSharedModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketTenantManagementDomainSharedModule> ();
            });

            Configure<RocketLocalizationOptions> (options => {
                options.Resources
                    .Add<RocketTenantManagementResource> ("en")
                    .AddBaseTypes (
                        typeof (RocketValidationResource)
                    ).AddVirtualJson ("/Aiwins/Rocket/TenantManagement/Localization/Resources");
            });
        }
    }
}