using System;
using Aiwins.Rocket.EntityFrameworkCore.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public static class RocketDbContextConfigurationContextPostgreSqlExtensions {
        [Obsolete ("Use 'UseNpgsql(...)' method instead. This will be removed in future versions.")]
        public static DbContextOptionsBuilder UsePostgreSql (
            [NotNull] this RocketDbContextConfigurationContext context, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null) {
            return context.UseNpgsql (postgreSqlOptionsAction);
        }

        public static DbContextOptionsBuilder UseNpgsql (
            [NotNull] this RocketDbContextConfigurationContext context, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null) {
            if (context.ExistingConnection != null) {
                return context.DbContextOptions.UseNpgsql (context.ExistingConnection, postgreSqlOptionsAction);
            } else {
                return context.DbContextOptions.UseNpgsql (context.ConnectionString, postgreSqlOptionsAction);
            }
        }
    }
}