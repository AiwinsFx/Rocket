using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public static class RocketDbContextOptionsSqliteExtensions {
        public static void UseSqlite (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<SqliteDbContextOptionsBuilder> sqliteOptionsAction = null) {
            options.Configure (context => {
                context.UseSqlite (sqliteOptionsAction);
            });
        }

        public static void UseSqlite<TDbContext> (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<SqliteDbContextOptionsBuilder> sqliteOptionsAction = null)
        where TDbContext : RocketDbContext<TDbContext> {
            options.Configure<TDbContext> (context => {
                context.UseSqlite (sqliteOptionsAction);
            });
        }
    }
}