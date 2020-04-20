using System;

namespace Aiwins.Rocket.MongoDB {
    public interface IMongoEntityModel {
        Type EntityType { get; }

        string CollectionName { get; }
    }
}