using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;

namespace Aiwins.Rocket.TenantManagement.MongoDB {
    [ConnectionStringName (RocketTenantManagementDbProperties.ConnectionStringName)]
    public interface ITenantManagementMongoDbContext : IRocketMongoDbContext {
        IMongoCollection<Tenant> Tenants { get; }
    }
}