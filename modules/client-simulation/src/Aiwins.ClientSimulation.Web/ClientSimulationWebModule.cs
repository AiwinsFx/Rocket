using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.Http.Client.IdentityModel.Web;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.ClientSimulation {
    [DependsOn (
        typeof (ClientSimulationModule),
        typeof (RocketHttpClientIdentityModelWebModule),
        typeof (RocketAspNetCoreMvcUiThemeSharedModule)
    )]
    public class ClientSimulationWebModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<ClientSimulationWebModule> ("Aiwins.ClientSimulation");
            });
        }
    }
}