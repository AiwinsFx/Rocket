using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Aiwins.Rocket.Uow.EntityFrameworkCore {
    public class EfCoreTransactionApi : ITransactionApi, ISupportsRollback {
        public IDbContextTransaction DbContextTransaction { get; }
        public IEfCoreDbContext StarterDbContext { get; }
        public List<IEfCoreDbContext> AttendedDbContexts { get; }

        public EfCoreTransactionApi (IDbContextTransaction dbContextTransaction, IEfCoreDbContext starterDbContext) {
            DbContextTransaction = dbContextTransaction;
            StarterDbContext = starterDbContext;
            AttendedDbContexts = new List<IEfCoreDbContext> ();
        }

        public void Commit () {
            DbContextTransaction.Commit ();

            foreach (var dbContext in AttendedDbContexts) {
                if (dbContext.As<DbContext> ().HasRelationalTransactionManager ()) {
                    continue; // 对于关系数据库使用共享事务
                }

                dbContext.Database.CommitTransaction ();
            }
        }

        public Task CommitAsync () {
            Commit ();
            return Task.CompletedTask;
        }

        public void Dispose () {
            DbContextTransaction.Dispose ();
        }

        public void Rollback () {
            DbContextTransaction.Rollback ();
        }

        public Task RollbackAsync (CancellationToken cancellationToken) {
            DbContextTransaction.Rollback ();
            return Task.CompletedTask;
        }
    }
}