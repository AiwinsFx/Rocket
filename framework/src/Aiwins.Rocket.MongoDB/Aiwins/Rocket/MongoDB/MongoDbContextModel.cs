using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.MongoDB {
    public class MongoDbContextModel {
        public IReadOnlyDictionary<Type, IMongoEntityModel> Entities { get; }

        public MongoDbContextModel (IReadOnlyDictionary<Type, IMongoEntityModel> entities) {
            Entities = entities;
        }
    }
}