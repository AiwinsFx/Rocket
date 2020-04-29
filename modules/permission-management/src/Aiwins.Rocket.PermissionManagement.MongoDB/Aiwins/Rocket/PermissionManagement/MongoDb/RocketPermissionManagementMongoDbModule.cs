using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.PermissionManagement.MongoDB {
    [DependsOn (
        typeof (RocketPermissionManagementDomainModule),
        typeof (RocketMongoDbModule)
    )]
    public class RocketPermissionManagementMongoDbModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddMongoDbContext<PermissionManagementMongoDbContext> (options => {
                options.AddDefaultRepositories<IPermissionManagementMongoDbContext> ();

                options.AddRepository<PermissionGrant, MongoPermissionGrantRepository> ();
            });
        }
    }
}