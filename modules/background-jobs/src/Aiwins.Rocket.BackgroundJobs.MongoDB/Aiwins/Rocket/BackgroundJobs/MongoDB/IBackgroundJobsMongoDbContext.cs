using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;

namespace Aiwins.Rocket.BackgroundJobs.MongoDB {
    [ConnectionStringName (BackgroundJobsDbProperties.ConnectionStringName)]
    public interface IBackgroundJobsMongoDbContext : IRocketMongoDbContext {
        IMongoCollection<BackgroundJobRecord> BackgroundJobs { get; }
    }
}