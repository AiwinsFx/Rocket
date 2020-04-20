using System.Collections.Generic;

namespace Aiwins.Rocket.Domain.Repositories.MemoryDb {
    public interface IMemoryDatabaseCollection<TEntity> : IEnumerable<TEntity> {
        void Add (TEntity entity);

        void Update (TEntity entity);

        void Remove (TEntity entity);
    }
}