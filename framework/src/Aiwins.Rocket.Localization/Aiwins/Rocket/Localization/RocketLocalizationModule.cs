using Aiwins.Rocket.Localization.Resources.RocketLocalization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Settings;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.Localization {
    [DependsOn (
        typeof (RocketVirtualFileSystemModule),
        typeof (RocketSettingsModule),
        typeof (RocketLocalizationAbstractionsModule)
    )]
    public class RocketLocalizationModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            RocketStringLocalizerFactory.Replace (context.Services);

            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketLocalizationModule> ("Aiwins.Rocket", "Aiwins/Rocket");
            });

            Configure<RocketLocalizationOptions> (options => {
                options
                    .Resources
                    .Add<DefaultResource> ("en");

                options
                    .Resources
                    .Add<RocketLocalizationResource> ("en")
                    .AddVirtualJson ("/Localization/Resources/RocketLocalization");
            });
        }
    }
}