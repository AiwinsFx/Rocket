using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.SettingManagement.Localization;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.SettingManagement {
    [DependsOn (typeof (RocketLocalizationModule))]
    public class RocketSettingManagementDomainSharedModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketSettingManagementDomainSharedModule> ();
            });

            Configure<RocketLocalizationOptions> (options => {
                options.Resources
                    .Add<RocketSettingManagementResource> ("en")
                    .AddVirtualJson ("/Aiwins/Rocket/SettingManagement/Localization/Resources/RocketSettingManagement");
            });
        }
    }
}