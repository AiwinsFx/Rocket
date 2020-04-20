using System;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.Domain.Repositories.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Aiwins.Rocket.Domain.Repositories {
    public static class MongoDbCoreRepositoryExtensions {
        public static IMongoDatabase GetDatabase<TEntity, TKey> (this IBasicRepository<TEntity, TKey> repository)
        where TEntity : class, IEntity<TKey> {
            return repository.ToMongoDbRepository ().Database;
        }

        public static IMongoCollection<TEntity> GetCollection<TEntity, TKey> (this IBasicRepository<TEntity, TKey> repository)
        where TEntity : class, IEntity<TKey> {
            return repository.ToMongoDbRepository ().Collection;
        }

        public static IMongoQueryable<TEntity> GetMongoQueryable<TEntity, TKey> (this IBasicRepository<TEntity, TKey> repository)
        where TEntity : class, IEntity<TKey> {
            return repository.ToMongoDbRepository ().GetMongoQueryable ();
        }

        public static IMongoDbRepository<TEntity, TKey> ToMongoDbRepository<TEntity, TKey> (this IBasicRepository<TEntity, TKey> repository)
        where TEntity : class, IEntity<TKey> {
            var mongoDbRepository = repository as IMongoDbRepository<TEntity, TKey>;
            if (mongoDbRepository == null) {
                throw new ArgumentException ("Given repository does not implement " + typeof (IMongoDbRepository<TEntity, TKey>).AssemblyQualifiedName, nameof (repository));
            }

            return mongoDbRepository;
        }
    }
}