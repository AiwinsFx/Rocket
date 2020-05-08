using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.IdentityServer.ApiResources;
using Aiwins.Rocket.IdentityServer.Clients;
using Aiwins.Rocket.IdentityServer.Devices;
using Aiwins.Rocket.IdentityServer.Grants;
using Aiwins.Rocket.IdentityServer.IdentityResources;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.IdentityServer.EntityFrameworkCore {
    [DependsOn (
        typeof (RocketIdentityServerDomainModule),
        typeof (RocketEntityFrameworkCoreModule)
    )]
    public class RocketIdentityServerEntityFrameworkCoreModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.PreConfigure<IIdentityServerBuilder> (
                builder => {
                    builder.AddRocketStores ();
                }
            );
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddRocketDbContext<IdentityServerDbContext> (options => {
                options.AddDefaultRepositories<IIdentityServerDbContext> ();

                options.AddRepository<Client, ClientRepository> ();
                options.AddRepository<ApiResource, ApiResourceRepository> ();
                options.AddRepository<IdentityResource, IdentityResourceRepository> ();
                options.AddRepository<PersistedGrant, PersistentGrantRepository> ();
                options.AddRepository<DeviceFlowCodes, DeviceFlowCodesRepository> ();
            });
        }
    }
}