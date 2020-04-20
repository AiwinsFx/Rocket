using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.RabbitMQ;
using Aiwins.Rocket.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.BackgroundJobs.RabbitMQ {
    [DependsOn (
        typeof (RocketBackgroundJobsAbstractionsModule),
        typeof (RocketRabbitMqModule),
        typeof (RocketThreadingModule)
    )]
    public class RocketBackgroundJobsRabbitMqModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddSingleton (typeof (IJobQueue<>), typeof (JobQueue<>));
        }

        public override void OnApplicationInitialization (ApplicationInitializationContext context) {
            StartJobQueueManager (context);
        }

        public override void OnApplicationShutdown (ApplicationShutdownContext context) {
            StopJobQueueManager (context);
        }

        private static void StartJobQueueManager (ApplicationInitializationContext context) {
            AsyncHelper.RunSync (
                () => context.ServiceProvider
                .GetRequiredService<IJobQueueManager> ()
                .StartAsync ()
            );
        }

        private static void StopJobQueueManager (ApplicationShutdownContext context) {
            AsyncHelper.RunSync (
                () => context.ServiceProvider
                .GetRequiredService<IJobQueueManager> ()
                .StopAsync ()
            );
        }
    }
}