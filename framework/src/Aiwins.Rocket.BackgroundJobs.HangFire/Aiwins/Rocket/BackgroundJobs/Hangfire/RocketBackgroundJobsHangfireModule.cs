using Aiwins.Rocket.Hangfire;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.BackgroundJobs.Hangfire {
    [DependsOn (
        typeof (RocketBackgroundJobsAbstractionsModule),
        typeof (RocketHangfireModule)
    )]
    public class RocketBackgroundJobsHangfireModule : RocketModule {

    }
}