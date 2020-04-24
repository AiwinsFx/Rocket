using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.BackgroundJobs.EntityFrameworkCore
{
    [DependsOn(
        typeof(RocketBackgroundJobsDomainModule),
        typeof(RocketEntityFrameworkCoreModule)
    )]
    public class RocketBackgroundJobsEntityFrameworkCoreModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddRocketDbContext<BackgroundJobsDbContext>(options =>
            {
                 options.AddRepository<BackgroundJobRecord, EfCoreBackgroundJobRepository>();
            });
        }
    }
}