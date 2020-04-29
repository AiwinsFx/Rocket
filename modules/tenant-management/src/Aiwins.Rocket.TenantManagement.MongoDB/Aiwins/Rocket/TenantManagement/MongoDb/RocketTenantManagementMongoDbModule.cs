using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.TenantManagement.MongoDB {
    [DependsOn (
        typeof (RocketTenantManagementDomainModule),
        typeof (RocketMongoDbModule)
    )]
    public class RocketTenantManagementMongoDbModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddMongoDbContext<TenantManagementMongoDbContext> (options => {
                options.AddDefaultRepositories<ITenantManagementMongoDbContext> ();

                options.AddRepository<Tenant, MongoTenantRepository> ();
            });
        }
    }
}