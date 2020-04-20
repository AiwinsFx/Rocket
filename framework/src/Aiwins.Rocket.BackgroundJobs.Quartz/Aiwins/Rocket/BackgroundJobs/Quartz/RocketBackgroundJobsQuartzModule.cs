using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Quartz;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.BackgroundJobs.Quartz {
    [DependsOn (
        typeof (RocketBackgroundJobsAbstractionsModule),
        typeof (RocketQuartzModule)
    )]
    public class RocketBackgroundJobsQuartzModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddTransient (typeof (QuartzJobExecutionAdapter<>));
        }
    }
}