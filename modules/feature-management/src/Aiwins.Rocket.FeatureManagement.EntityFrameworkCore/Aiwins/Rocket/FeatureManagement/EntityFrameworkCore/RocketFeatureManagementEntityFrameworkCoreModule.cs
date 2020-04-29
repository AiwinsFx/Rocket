using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.FeatureManagement.EntityFrameworkCore {
    [DependsOn (
        typeof (RocketFeatureManagementDomainModule),
        typeof (RocketEntityFrameworkCoreModule)
    )]
    public class RocketFeatureManagementEntityFrameworkCoreModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddRocketDbContext<FeatureManagementDbContext> (options => {
                options.AddDefaultRepositories<IFeatureManagementDbContext> ();

                options.AddRepository<FeatureValue, EfCoreFeatureValueRepository> ();
            });
        }
    }
}