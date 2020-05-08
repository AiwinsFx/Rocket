using Aiwins.Rocket.IdentityServer.Devices;
using Aiwins.Rocket.IdentityServer.Grants;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using ApiResource = Aiwins.Rocket.IdentityServer.ApiResources.ApiResource;
using Client = Aiwins.Rocket.IdentityServer.Clients.Client;
using IdentityResource = Aiwins.Rocket.IdentityServer.IdentityResources.IdentityResource;

namespace Aiwins.Rocket.IdentityServer.MongoDB {
    [DependsOn (
        typeof (RocketIdentityServerDomainModule),
        typeof (RocketMongoDbModule)
    )]
    public class RocketIdentityServerMongoDbModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.PreConfigure<IIdentityServerBuilder> (
                builder => {
                    builder.AddRocketStores ();
                }
            );
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddMongoDbContext<RocketIdentityServerMongoDbContext> (options => {
                options.AddRepository<ApiResource, MongoApiResourceRepository> ();
                options.AddRepository<IdentityResource, MongoIdentityResourceRepository> ();
                options.AddRepository<Client, MongoClientRepository> ();
                options.AddRepository<PersistedGrant, MongoPersistentGrantRepository> ();
                options.AddRepository<DeviceFlowCodes, MongoDeviceFlowCodesRepository> ();
            });
        }
    }
}