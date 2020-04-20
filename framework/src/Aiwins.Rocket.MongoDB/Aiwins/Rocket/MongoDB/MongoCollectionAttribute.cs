using System;

namespace Aiwins.Rocket.MongoDB {
    public class MongoCollectionAttribute : Attribute {
        public string CollectionName { get; set; }

        public MongoCollectionAttribute () {

        }

        public MongoCollectionAttribute (string collectionName) {
            CollectionName = collectionName;
        }
    }
}