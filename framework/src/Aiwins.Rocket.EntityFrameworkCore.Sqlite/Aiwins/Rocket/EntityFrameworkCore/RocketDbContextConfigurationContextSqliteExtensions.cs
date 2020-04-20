using System;
using Aiwins.Rocket.EntityFrameworkCore.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public static class RocketDbContextConfigurationContextSqliteExtensions {
        public static DbContextOptionsBuilder UseSqlite (
            [NotNull] this RocketDbContextConfigurationContext context, [CanBeNull] Action<SqliteDbContextOptionsBuilder> sqliteOptionsAction = null) {
            if (context.ExistingConnection != null) {
                return context.DbContextOptions.UseSqlite (context.ExistingConnection, sqliteOptionsAction);
            } else {
                return context.DbContextOptions.UseSqlite (context.ConnectionString, sqliteOptionsAction);
            }
        }
    }
}