using MongoDB.Driver;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.TenantManagement.MongoDB
{
    [ConnectionStringName(RocketTenantManagementDbProperties.ConnectionStringName)]
    public interface ITenantManagementMongoDbContext : IRocketMongoDbContext
    {
        IMongoCollection<Tenant> Tenants { get; }
    }
}