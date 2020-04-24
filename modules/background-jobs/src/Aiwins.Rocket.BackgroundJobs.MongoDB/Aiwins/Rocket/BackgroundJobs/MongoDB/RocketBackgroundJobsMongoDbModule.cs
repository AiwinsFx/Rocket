using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.BackgroundJobs.MongoDB
{
    [DependsOn(
        typeof(RocketBackgroundJobsDomainModule),
        typeof(RocketMongoDbModule)
        )]
    public class RocketBackgroundJobsMongoDbModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<BackgroundJobsMongoDbContext>(options =>
            {
                 options.AddRepository<BackgroundJobRecord, MongoBackgroundJobRepository>();
            });
        }
    }
}
