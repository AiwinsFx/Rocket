using System;
using System.Collections.Generic;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.Domain.Repositories.MemoryDb;

namespace Aiwins.Rocket.Domain.Repositories {
    public static class MemoryDbCoreRepositoryExtensions {
        public static IMemoryDatabase GetDatabase<TEntity, TKey> (this IBasicRepository<TEntity, TKey> repository)
        where TEntity : class, IEntity<TKey> {
            return repository.ToMemoryDbRepository ().Database;
        }

        public static IMemoryDatabaseCollection<TEntity> GetCollection<TEntity, TKey> (this IBasicRepository<TEntity, TKey> repository)
        where TEntity : class, IEntity<TKey> {
            return repository.ToMemoryDbRepository ().Collection;
        }

        public static IMemoryDbRepository<TEntity, TKey> ToMemoryDbRepository<TEntity, TKey> (this IBasicRepository<TEntity, TKey> repository)
        where TEntity : class, IEntity<TKey> {
            var memoryDbRepository = repository as IMemoryDbRepository<TEntity, TKey>;
            if (memoryDbRepository == null) {
                throw new ArgumentException ("Given repository does not implement " + typeof (IMemoryDbRepository<TEntity, TKey>).AssemblyQualifiedName, nameof (repository));
            }

            return memoryDbRepository;
        }
    }
}