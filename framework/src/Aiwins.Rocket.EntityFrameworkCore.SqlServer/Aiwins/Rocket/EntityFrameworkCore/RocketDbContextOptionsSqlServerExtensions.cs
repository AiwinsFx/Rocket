using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public static class RocketDbContextOptionsSqlServerExtensions {
        public static void UseSqlServer (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null) {
            options.Configure (context => {
                context.UseSqlServer (sqlServerOptionsAction);
            });
        }

        public static void UseSqlServer<TDbContext> (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
        where TDbContext : RocketDbContext<TDbContext> {
            options.Configure<TDbContext> (context => {
                context.UseSqlServer (sqlServerOptionsAction);
            });
        }
    }
}