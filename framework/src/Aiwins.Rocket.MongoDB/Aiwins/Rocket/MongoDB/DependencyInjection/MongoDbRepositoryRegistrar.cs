using System;
using System.Collections.Generic;
using Aiwins.Rocket.Domain.Repositories;
using Aiwins.Rocket.Domain.Repositories.MongoDB;

namespace Aiwins.Rocket.MongoDB.DependencyInjection {
    public class MongoDbRepositoryRegistrar : RepositoryRegistrarBase<RocketMongoDbContextRegistrationOptions> {
        public MongoDbRepositoryRegistrar (RocketMongoDbContextRegistrationOptions options) : base (options) {

        }

        protected override IEnumerable<Type> GetEntityTypes (Type dbContextType) {
            return MongoDbContextHelper.GetEntityTypes (dbContextType);
        }

        protected override Type GetRepositoryType (Type dbContextType, Type entityType) {
            return typeof (MongoDbRepository<,>).MakeGenericType (dbContextType, entityType);
        }

        protected override Type GetRepositoryType (Type dbContextType, Type entityType, Type primaryKeyType) {
            return typeof (MongoDbRepository<, ,>).MakeGenericType (dbContextType, entityType, primaryKeyType);
        }
    }
}