using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.EventBus.RabbitMq {
    [DependsOn (
        typeof (RocketEventBusModule),
        typeof (RocketRabbitMqModule))]
    public class RocketEventBusRabbitMqModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            var configuration = context.Services.GetConfiguration ();

            Configure<RocketRabbitMqEventBusOptions> (configuration.GetSection ("RabbitMQ:EventBus"));
        }

        public override void OnApplicationInitialization (ApplicationInitializationContext context) {
            context
                .ServiceProvider
                .GetRequiredService<RabbitMqDistributedEventBus> ()
                .Initialize ();
        }
    }
}