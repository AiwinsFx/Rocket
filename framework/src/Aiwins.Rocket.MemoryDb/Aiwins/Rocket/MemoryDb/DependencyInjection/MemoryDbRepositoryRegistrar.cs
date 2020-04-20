using System;
using System.Collections.Generic;
using Aiwins.Rocket.Domain.Repositories;
using Aiwins.Rocket.Domain.Repositories.MemoryDb;

namespace Aiwins.Rocket.MemoryDb.DependencyInjection {
    public class MemoryDbRepositoryRegistrar : RepositoryRegistrarBase<RocketMemoryDbContextRegistrationOptions> {
        public MemoryDbRepositoryRegistrar (RocketMemoryDbContextRegistrationOptions options) : base (options) { }

        protected override IEnumerable<Type> GetEntityTypes (Type dbContextType) {
            var memoryDbContext = (MemoryDbContext) Activator.CreateInstance (dbContextType);
            return memoryDbContext.GetEntityTypes ();
        }

        protected override Type GetRepositoryType (Type dbContextType, Type entityType) {
            return typeof (MemoryDbRepository<,>).MakeGenericType (dbContextType, entityType);
        }

        protected override Type GetRepositoryType (Type dbContextType, Type entityType, Type primaryKeyType) {
            return typeof (MemoryDbRepository<, ,>).MakeGenericType (dbContextType, entityType, primaryKeyType);
        }
    }
}