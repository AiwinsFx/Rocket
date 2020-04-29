using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.SettingManagement.MongoDB {
    [DependsOn (
        typeof (RocketSettingManagementDomainModule),
        typeof (RocketMongoDbModule)
    )]
    public class RocketSettingManagementMongoDbModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddMongoDbContext<SettingManagementMongoDbContext> (options => {
                options.AddDefaultRepositories<ISettingManagementMongoDbContext> ();

                options.AddRepository<Setting, MongoSettingRepository> ();
            });
        }
    }
}