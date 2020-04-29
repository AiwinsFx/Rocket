using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.PermissionManagement.EntityFrameworkCore {
    [DependsOn (typeof (RocketPermissionManagementDomainModule))]
    [DependsOn (typeof (RocketEntityFrameworkCoreModule))]
    public class RocketPermissionManagementEntityFrameworkCoreModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddRocketDbContext<PermissionManagementDbContext> (options => {
                options.AddDefaultRepositories<IPermissionManagementDbContext> ();

                options.AddRepository<PermissionGrant, EfCorePermissionGrantRepository> ();
            });
        }
    }
}