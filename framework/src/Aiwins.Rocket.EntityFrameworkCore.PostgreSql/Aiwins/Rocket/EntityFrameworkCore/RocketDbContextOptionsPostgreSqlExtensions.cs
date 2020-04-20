using System;
using JetBrains.Annotations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public static class RocketDbContextOptionsPostgreSqlExtensions {
        public static void UsePostgreSql (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null) {
            options.Configure (context => {
                context.UsePostgreSql (postgreSqlOptionsAction);
            });
        }

        public static void UsePostgreSql<TDbContext> (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null)
        where TDbContext : RocketDbContext<TDbContext> {
            options.Configure<TDbContext> (context => {
                context.UsePostgreSql (postgreSqlOptionsAction);
            });
        }
    }
}