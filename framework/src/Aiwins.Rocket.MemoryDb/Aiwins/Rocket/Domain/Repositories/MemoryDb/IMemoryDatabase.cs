using Aiwins.Rocket.Domain.Entities;

namespace Aiwins.Rocket.Domain.Repositories.MemoryDb {
    public interface IMemoryDatabase {
        IMemoryDatabaseCollection<TEntity> Collection<TEntity> () where TEntity : class, IEntity;

        TKey GenerateNextId<TEntity, TKey> ();
    }
}