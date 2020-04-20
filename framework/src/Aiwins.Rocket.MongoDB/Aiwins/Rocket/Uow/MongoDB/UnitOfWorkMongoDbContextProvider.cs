using System;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Aiwins.Rocket.Uow.MongoDB {
    public class UnitOfWorkMongoDbContextProvider<TMongoDbContext> : IMongoDbContextProvider<TMongoDbContext>
        where TMongoDbContext : IRocketMongoDbContext {
            private readonly IUnitOfWorkManager _unitOfWorkManager;
            private readonly IConnectionStringResolver _connectionStringResolver;

            public UnitOfWorkMongoDbContextProvider (
                IUnitOfWorkManager unitOfWorkManager,
                IConnectionStringResolver connectionStringResolver) {
                _unitOfWorkManager = unitOfWorkManager;
                _connectionStringResolver = connectionStringResolver;
            }

            public TMongoDbContext GetDbContext () {
                var unitOfWork = _unitOfWorkManager.Current;
                if (unitOfWork == null) {
                    throw new RocketException ($"A {nameof(IMongoDatabase)} instance can only be created inside a unit of work!");
                }

                var connectionString = _connectionStringResolver.Resolve<TMongoDbContext> ();
                var dbContextKey = $"{typeof(TMongoDbContext).FullName}_{connectionString}";

                var mongoUrl = new MongoUrl (connectionString);
                var databaseName = mongoUrl.DatabaseName;
                if (databaseName.IsNullOrWhiteSpace ()) {
                    databaseName = ConnectionStringNameAttribute.GetConnStringName<TMongoDbContext> ();
                }

                //TODO: 考虑创建一个单例的客户端 MongoDbClient (除了作为示例 MongoClientCache).
                var databaseApi = unitOfWork.GetOrAddDatabaseApi (
                    dbContextKey,
                    () => {
                        var database = new MongoClient (mongoUrl).GetDatabase (databaseName);

                        var dbContext = unitOfWork.ServiceProvider.GetRequiredService<TMongoDbContext> ();

                        dbContext.ToRocketMongoDbContext ().InitializeDatabase (database);

                        return new MongoDbDatabaseApi<TMongoDbContext> (dbContext);
                    });

                return ((MongoDbDatabaseApi<TMongoDbContext>) databaseApi).DbContext;
            }
        }
}