using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.BackgroundJobs.MongoDB {
    [DependsOn (
        typeof (RocketBackgroundJobsDomainModule),
        typeof (RocketMongoDbModule)
    )]
    public class RocketBackgroundJobsMongoDbModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddMongoDbContext<BackgroundJobsMongoDbContext> (options => {
                options.AddRepository<BackgroundJobRecord, MongoBackgroundJobRepository> ();
            });
        }
    }
}