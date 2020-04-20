using Aiwins.Rocket.Domain.Repositories.MemoryDb;

namespace Aiwins.Rocket.Uow.MemoryDb {
    public class MemoryDbDatabaseApi : IDatabaseApi {
        public IMemoryDatabase Database { get; }

        public MemoryDbDatabaseApi (IMemoryDatabase database) {
            Database = database;
        }
    }
}