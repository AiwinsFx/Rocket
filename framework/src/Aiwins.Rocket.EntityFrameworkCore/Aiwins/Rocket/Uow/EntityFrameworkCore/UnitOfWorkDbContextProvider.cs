using System;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Uow.EntityFrameworkCore {
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : IEfCoreDbContext {
            private readonly IUnitOfWorkManager _unitOfWorkManager;
            private readonly IConnectionStringResolver _connectionStringResolver;

            public UnitOfWorkDbContextProvider (
                IUnitOfWorkManager unitOfWorkManager,
                IConnectionStringResolver connectionStringResolver) {
                _unitOfWorkManager = unitOfWorkManager;
                _connectionStringResolver = connectionStringResolver;
            }

            public TDbContext GetDbContext () {
                var unitOfWork = _unitOfWorkManager.Current;
                if (unitOfWork == null) {
                    throw new RocketException ("A DbContext can only be created inside a unit of work!");
                }

                var connectionStringName = ConnectionStringNameAttribute.GetConnStringName<TDbContext> ();
                var connectionString = _connectionStringResolver.Resolve (connectionStringName);

                var dbContextKey = $"{typeof(TDbContext).FullName}_{connectionString}";

                var databaseApi = unitOfWork.GetOrAddDatabaseApi (
                    dbContextKey,
                    () => new EfCoreDatabaseApi<TDbContext> (
                        CreateDbContext (unitOfWork, connectionStringName, connectionString)
                    ));

                return ((EfCoreDatabaseApi<TDbContext>) databaseApi).DbContext;
            }

            private TDbContext CreateDbContext (IUnitOfWork unitOfWork, string connectionStringName, string connectionString) {
                var creationContext = new DbContextCreationContext (connectionStringName, connectionString);
                using (DbContextCreationContext.Use (creationContext)) {
                    var dbContext = CreateDbContext (unitOfWork);

                    if (dbContext is IRocketEfCoreDbContext rocketEfCoreDbContext) {
                        rocketEfCoreDbContext.Initialize (
                            new RocketEfCoreDbContextInitializationContext (
                                unitOfWork
                            )
                        );
                    }

                    return dbContext;
                }
            }

            private TDbContext CreateDbContext (IUnitOfWork unitOfWork) {
                return unitOfWork.Options.IsTransactional ?
                    CreateDbContextWithTransaction (unitOfWork) :
                    unitOfWork.ServiceProvider.GetRequiredService<TDbContext> ();
            }

            public TDbContext CreateDbContextWithTransaction (IUnitOfWork unitOfWork) {
                var transactionApiKey = $"EntityFrameworkCore_{DbContextCreationContext.Current.ConnectionString}";
                var activeTransaction = unitOfWork.FindTransactionApi (transactionApiKey) as EfCoreTransactionApi;

                if (activeTransaction == null) {
                    var dbContext = unitOfWork.ServiceProvider.GetRequiredService<TDbContext> ();

                    var dbtransaction = unitOfWork.Options.IsolationLevel.HasValue ?
                        dbContext.Database.BeginTransaction (unitOfWork.Options.IsolationLevel.Value) :
                        dbContext.Database.BeginTransaction ();

                    unitOfWork.AddTransactionApi (
                        transactionApiKey,
                        new EfCoreTransactionApi (
                            dbtransaction,
                            dbContext
                        )
                    );

                    return dbContext;
                } else {
                    DbContextCreationContext.Current.ExistingConnection = activeTransaction.DbContextTransaction.GetDbTransaction ().Connection;

                    var dbContext = unitOfWork.ServiceProvider.GetRequiredService<TDbContext> ();

                    if (dbContext.As<DbContext> ().HasRelationalTransactionManager ()) {
                        dbContext.Database.UseTransaction (activeTransaction.DbContextTransaction.GetDbTransaction ());
                    } else {
                        dbContext.Database.BeginTransaction (); //TODO: 此处考虑创建新的事务
                    }

                    activeTransaction.AttendedDbContexts.Add (dbContext);

                    return dbContext;
                }
            }
        }
}