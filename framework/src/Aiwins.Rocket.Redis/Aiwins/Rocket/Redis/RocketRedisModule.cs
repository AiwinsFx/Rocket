using Aiwins.Rocket.Json;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Redis {
    [DependsOn (
        typeof (RocketJsonModule),
        typeof (RocketThreadingModule)
    )]
    public class RocketRedisModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            var configuration = context.Services.GetConfiguration ();
            Configure<RocketRedisOptions> (configuration.GetSection ("Redis"));
        }

        public override void OnApplicationShutdown (ApplicationShutdownContext context) {
            context.ServiceProvider
                .GetRequiredService<IChannelPool> ()
                .Dispose ();

            context.ServiceProvider
                .GetRequiredService<IConnectionPool> ()
                .Dispose ();
        }
    }
}