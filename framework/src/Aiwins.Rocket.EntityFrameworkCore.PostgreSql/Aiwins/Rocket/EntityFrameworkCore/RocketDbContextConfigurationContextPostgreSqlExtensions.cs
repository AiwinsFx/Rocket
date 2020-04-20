using System;
using Aiwins.Rocket.EntityFrameworkCore.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public static class RocketDbContextConfigurationContextPostgreSqlExtensions {
        public static DbContextOptionsBuilder UsePostgreSql (
            [NotNull] this RocketDbContextConfigurationContext context, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null) {
            if (context.ExistingConnection != null) {
                return context.DbContextOptions.UseNpgsql (context.ExistingConnection, postgreSqlOptionsAction);
            } else {
                return context.DbContextOptions.UseNpgsql (context.ConnectionString, postgreSqlOptionsAction);
            }
        }
    }
}