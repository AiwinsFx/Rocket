using System;
using JetBrains.Annotations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public static class RocketDbContextOptionsPostgreSqlExtensions {
        [Obsolete ("Use 'UseNpgsql(...)' method instead. This will be removed in future versions.")]
        public static void UsePostgreSql (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null) {
            options.Configure (context => {
                context.UseNpgsql (postgreSqlOptionsAction);
            });
        }

        [Obsolete ("Use 'UseNpgsql(...)' method instead. This will be removed in future versions.")]
        public static void UsePostgreSql<TDbContext> (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null)
        where TDbContext : RocketDbContext<TDbContext> {
            options.Configure<TDbContext> (context => {
                context.UseNpgsql (postgreSqlOptionsAction);
            });
        }

        public static void UseNpgsql (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null) {
            options.Configure (context => {
                context.UseNpgsql (postgreSqlOptionsAction);
            });
        }

        public static void UseNpgsql<TDbContext> (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null)
        where TDbContext : RocketDbContext<TDbContext> {
            options.Configure<TDbContext> (context => {
                context.UseNpgsql (postgreSqlOptionsAction);
            });
        }
    }
}