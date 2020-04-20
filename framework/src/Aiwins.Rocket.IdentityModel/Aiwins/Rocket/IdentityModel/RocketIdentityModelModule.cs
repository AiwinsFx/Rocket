using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.IdentityModel {
    [DependsOn (
        typeof (RocketThreadingModule)
    )]
    public class RocketIdentityModelModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            var configuration = context.Services.GetConfiguration ();

            context.Services.AddHttpClient ();

            Configure<RocketIdentityClientOptions> (configuration);
        }
    }
}