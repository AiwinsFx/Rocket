using MongoDB.Driver;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.SettingManagement.MongoDB
{
    [ConnectionStringName(RocketSettingManagementDbProperties.ConnectionStringName)]
    public interface ISettingManagementMongoDbContext : IRocketMongoDbContext
    {
        IMongoCollection<Setting> Settings { get; }
    }
}