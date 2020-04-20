using System;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.Domain.Repositories {
    //TODO: 考虑扩展方法适用于任何Repository服务

    public static class EfCoreRepositoryExtensions {
        public static DbContext GetDbContext<TEntity, TKey> (this IReadOnlyBasicRepository<TEntity, TKey> repository)
        where TEntity : class, IEntity<TKey> {
            return repository.ToEfCoreRepository ().DbContext;
        }

        public static DbSet<TEntity> GetDbSet<TEntity, TKey> (this IReadOnlyBasicRepository<TEntity, TKey> repository)
        where TEntity : class, IEntity<TKey> {
            return repository.ToEfCoreRepository ().DbSet;
        }

        public static IEfCoreRepository<TEntity, TKey> ToEfCoreRepository<TEntity, TKey> (this IReadOnlyBasicRepository<TEntity, TKey> repository)
        where TEntity : class, IEntity<TKey> {
            var efCoreRepository = repository as IEfCoreRepository<TEntity, TKey>;
            if (efCoreRepository == null) {
                throw new ArgumentException ("Given repository does not implement " + typeof (IEfCoreRepository<TEntity, TKey>).AssemblyQualifiedName, nameof (repository));
            }

            return efCoreRepository;
        }
    }
}