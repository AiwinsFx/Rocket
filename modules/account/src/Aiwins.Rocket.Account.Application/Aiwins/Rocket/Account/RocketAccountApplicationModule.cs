using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.UI.Navigation.Urls;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.Account {
    [DependsOn (
        typeof (RocketAccountApplicationContractsModule),
        typeof (RocketIdentityApplicationModule),
        typeof (RocketUiNavigationModule)
    )]
    public class RocketAccountApplicationModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketAccountApplicationModule> ();
            });
        }
    }
}