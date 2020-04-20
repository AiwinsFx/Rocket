using System;
using System.Collections.Generic;
using Aiwins.Rocket.Domain.Repositories;
using Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore;

namespace Aiwins.Rocket.EntityFrameworkCore.DependencyInjection {
    public class EfCoreRepositoryRegistrar : RepositoryRegistrarBase<RocketDbContextRegistrationOptions> {
        public EfCoreRepositoryRegistrar (RocketDbContextRegistrationOptions options) : base (options) {

        }

        protected override IEnumerable<Type> GetEntityTypes (Type dbContextType) {
            return DbContextHelper.GetEntityTypes (dbContextType);
        }

        protected override Type GetRepositoryType (Type dbContextType, Type entityType) {
            return typeof (EfCoreRepository<,>).MakeGenericType (dbContextType, entityType);
        }

        protected override Type GetRepositoryType (Type dbContextType, Type entityType, Type primaryKeyType) {
            return typeof (EfCoreRepository<, ,>).MakeGenericType (dbContextType, entityType, primaryKeyType);
        }
    }
}