using MongoDB.Driver;

namespace Aiwins.Rocket.MongoDB {
    public interface IRocketMongoDbContext {
        IMongoDatabase Database { get; }

        IMongoCollection<T> Collection<T> ();
    }
}