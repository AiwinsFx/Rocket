using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Quartz;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aiwins.Rocket.BackgroundWorkers.Quartz {
    [DependsOn (
        typeof (RocketBackgroundWorkersModule),
        typeof (RocketQuartzModule)
    )]
    public class RocketBackgroundWorkersQuartzModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddConventionalRegistrar (new RocketQuartzConventionalRegistrar ());
        }

        public override void OnApplicationInitialization (ApplicationInitializationContext context) {
            var backgroundWorkerManager = context.ServiceProvider.GetService<IBackgroundWorkerManager> ();
            var works = context.ServiceProvider.GetServices<IQuartzBackgroundWorker> ();

            foreach (var work in works) {
                backgroundWorkerManager.Add (work);
            }
        }
    }
}