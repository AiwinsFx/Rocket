using MongoDB.Driver;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.PermissionManagement.MongoDB
{
    [ConnectionStringName(RocketPermissionManagementDbProperties.ConnectionStringName)]
    public interface IPermissionManagementMongoDbContext : IRocketMongoDbContext
    {
        IMongoCollection<PermissionGrant> PermissionGrants { get; }
    }
}