using MongoDB.Bson.Serialization;

namespace Aiwins.Rocket.MongoDB {
    public interface IHasBsonClassMap {
        BsonClassMap GetMap ();
    }
}