using Aiwins.Rocket.Json;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.RabbitMQ {
    [DependsOn (
        typeof (AbpJsonModule),
        typeof (AbpThreadingModule)
    )]
    public class AbpRabbitMqModule : AbpModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            var configuration = context.Services.GetConfiguration ();
            Configure<AbpRabbitMqOptions> (configuration.GetSection ("RabbitMQ"));
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