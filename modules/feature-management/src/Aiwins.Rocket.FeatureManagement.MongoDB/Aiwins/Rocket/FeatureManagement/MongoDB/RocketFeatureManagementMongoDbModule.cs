using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.FeatureManagement.MongoDB {
    [DependsOn (
        typeof (RocketFeatureManagementDomainModule),
        typeof (RocketMongoDbModule)
    )]
    public class RocketFeatureManagementMongoDbModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddMongoDbContext<FeatureManagementMongoDbContext> (options => {
                options.AddDefaultRepositories<IFeatureManagementMongoDbContext> ();

                options.AddRepository<FeatureValue, MongoFeatureValueRepository> ();
            });
        }
    }
}