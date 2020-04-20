using Aiwins.Rocket.Application.Localization.Resources.RocketDdd;
using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.Application {
    [DependsOn (
        typeof (RocketAuditingModule),
        typeof (RocketLocalizationModule)
    )]
    public class RocketDddApplicationContractsModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketDddApplicationContractsModule> ();
            });

            Configure<RocketLocalizationOptions> (options => {
                options.Resources
                    .Add<RocketDddApplicationContractsResource> ("en")
                    .AddVirtualJson ("/Aiwins/Rocket/Application/Localization/Resources/RocketDdd");
            });
        }
    }
}