using Aiwins.Rocket.Data;
using Aiwins.Rocket.IdentityServer.ApiResources;
using Aiwins.Rocket.IdentityServer.Clients;
using Aiwins.Rocket.IdentityServer.Devices;
using Aiwins.Rocket.IdentityServer.Grants;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;
using IdentityResource = Aiwins.Rocket.IdentityServer.IdentityResources.IdentityResource;

namespace Aiwins.Rocket.IdentityServer.MongoDB {
    [ConnectionStringName (RocketIdentityServerDbProperties.ConnectionStringName)]
    public class RocketIdentityServerMongoDbContext : RocketMongoDbContext, IRocketIdentityServerMongoDbContext {
        public IMongoCollection<ApiResource> ApiResources => Collection<ApiResource> ();

        public IMongoCollection<Client> Clients => Collection<Client> ();

        public IMongoCollection<IdentityResource> IdentityResources => Collection<IdentityResource> ();

        public IMongoCollection<PersistedGrant> PersistedGrants => Collection<PersistedGrant> ();

        public IMongoCollection<DeviceFlowCodes> DeviceFlowCodes => Collection<DeviceFlowCodes> ();

        protected override void CreateModel (IMongoModelBuilder modelBuilder) {
            base.CreateModel (modelBuilder);

            modelBuilder.ConfigureIdentityServer ();
        }
    }
}