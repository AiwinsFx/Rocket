using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.EventBus.Redis {
    [DependsOn (
        typeof (RocketEventBusModule),
        typeof (RocketRedisModule))]
    public class RocketEventBusRedisModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            var configuration = context.Services.GetConfiguration ();

            Configure<RocketRedisEventBusOptions> (configuration.GetSection ("Redis:EventBus"));
        }
    }
}