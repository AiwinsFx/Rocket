using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;

namespace Aiwins.Rocket.FeatureManagement.MongoDB {
    [ConnectionStringName (FeatureManagementDbProperties.ConnectionStringName)]
    public interface IFeatureManagementMongoDbContext : IRocketMongoDbContext {
        IMongoCollection<FeatureValue> FeatureValues { get; }
    }
}