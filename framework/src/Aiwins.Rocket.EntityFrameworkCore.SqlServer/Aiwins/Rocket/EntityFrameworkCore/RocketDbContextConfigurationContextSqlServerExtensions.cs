using System;
using Aiwins.Rocket.EntityFrameworkCore.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public static class RocketDbContextConfigurationContextSqlServerExtensions {
        public static DbContextOptionsBuilder UseSqlServer (
            [NotNull] this RocketDbContextConfigurationContext context, [CanBeNull] Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null) {
            if (context.ExistingConnection != null) {
                return context.DbContextOptions.UseSqlServer (context.ExistingConnection, sqlServerOptionsAction);
            } else {
                return context.DbContextOptions.UseSqlServer (context.ConnectionString, sqlServerOptionsAction);
            }
        }
    }
}