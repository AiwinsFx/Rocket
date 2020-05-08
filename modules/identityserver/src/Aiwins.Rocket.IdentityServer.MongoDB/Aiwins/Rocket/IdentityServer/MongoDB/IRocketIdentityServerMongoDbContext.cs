using Aiwins.Rocket.Data;
using Aiwins.Rocket.IdentityServer.Clients;
using Aiwins.Rocket.IdentityServer.Devices;
using Aiwins.Rocket.IdentityServer.Grants;
using Aiwins.Rocket.IdentityServer.IdentityResources;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;
using ApiResource = Aiwins.Rocket.IdentityServer.ApiResources.ApiResource;

namespace Aiwins.Rocket.IdentityServer.MongoDB {
    [ConnectionStringName (RocketIdentityServerDbProperties.ConnectionStringName)]
    public interface IRocketIdentityServerMongoDbContext : IRocketMongoDbContext {
        IMongoCollection<ApiResource> ApiResources { get; }

        IMongoCollection<Client> Clients { get; }

        IMongoCollection<IdentityResource> IdentityResources { get; }

        IMongoCollection<PersistedGrant> PersistedGrants { get; }

        IMongoCollection<DeviceFlowCodes> DeviceFlowCodes { get; }
    }
}