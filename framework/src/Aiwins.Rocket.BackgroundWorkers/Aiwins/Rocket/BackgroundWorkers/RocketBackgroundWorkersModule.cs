using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.BackgroundWorkers {
    [DependsOn (
        typeof (RocketThreadingModule)
    )]
    public class RocketBackgroundWorkersModule : RocketModule {
        public override void OnApplicationInitialization (ApplicationInitializationContext context) {
            var options = context.ServiceProvider.GetRequiredService<IOptions<RocketBackgroundWorkerOptions>> ().Value;
            if (options.IsEnabled) {
                AsyncHelper.RunSync (
                    () => context.ServiceProvider
                    .GetRequiredService<IBackgroundWorkerManager> ()
                    .StartAsync ()
                );
            }
        }

        public override void OnApplicationShutdown (ApplicationShutdownContext context) {
            var options = context.ServiceProvider.GetRequiredService<IOptions<RocketBackgroundWorkerOptions>> ().Value;
            if (options.IsEnabled) {
                AsyncHelper.RunSync (
                    () => context.ServiceProvider
                    .GetRequiredService<IBackgroundWorkerManager> ()
                    .StopAsync ()
                );
            }
        }
    }
}