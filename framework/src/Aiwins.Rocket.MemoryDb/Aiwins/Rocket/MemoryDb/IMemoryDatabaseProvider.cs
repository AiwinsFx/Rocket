using Aiwins.Rocket.Domain.Repositories.MemoryDb;

namespace Aiwins.Rocket.MemoryDb {
    public interface IMemoryDatabaseProvider<TMemoryDbContext>
        where TMemoryDbContext : MemoryDbContext {
            TMemoryDbContext DbContext { get; }

            IMemoryDatabase GetDatabase ();
        }
}