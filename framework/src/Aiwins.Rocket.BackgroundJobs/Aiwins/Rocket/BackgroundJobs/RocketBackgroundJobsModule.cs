using Aiwins.Rocket.BackgroundWorkers;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Timing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.BackgroundJobs {
    [DependsOn (
        typeof (RocketBackgroundJobsAbstractionsModule),
        typeof (RocketBackgroundWorkersModule),
        typeof (RocketTimingModule),
        typeof (RocketGuidsModule)
    )]
    public class RocketBackgroundJobsModule : RocketModule {
        public override void OnApplicationInitialization (ApplicationInitializationContext context) {
            var options = context.ServiceProvider.GetRequiredService<IOptions<RocketBackgroundJobOptions>> ().Value;
            if (options.IsJobExecutionEnabled) {
                context.ServiceProvider
                    .GetRequiredService<IBackgroundWorkerManager> ()
                    .Add (
                        context.ServiceProvider
                        .GetRequiredService<IBackgroundJobWorker> ()
                    );
            }
        }
    }
}