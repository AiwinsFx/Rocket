using Aiwins.Rocket.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore {
    public interface IEfCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity {
            DbContext DbContext { get; }

            DbSet<TEntity> DbSet { get; }
        }

    public interface IEfCoreRepository<TEntity, TKey> : IEfCoreRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey> {

        }
}