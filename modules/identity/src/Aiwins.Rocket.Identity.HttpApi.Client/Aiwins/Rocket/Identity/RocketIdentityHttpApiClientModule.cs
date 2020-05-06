using Aiwins.Rocket.Http.Client;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Identity {
    [DependsOn (
        typeof (RocketIdentityApplicationContractsModule),
        typeof (RocketHttpClientModule))]
    public class RocketIdentityHttpApiClientModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddHttpClientProxies (
                typeof (RocketIdentityApplicationContractsModule).Assembly,
                IdentityRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}