using Aiwins.Rocket.Castle;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.Threading;
using Aiwins.Rocket.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Http.Client {
    [DependsOn (
        typeof (RocketHttpModule),
        typeof (RocketCastleCoreModule),
        typeof (RocketThreadingModule),
        typeof (RocketMultiTenancyModule),
        typeof (RocketValidationModule)
    )]
    public class RocketHttpClientModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            var configuration = context.Services.GetConfiguration ();
            Configure<RocketRemoteServiceOptions> (configuration);
        }
    }
}