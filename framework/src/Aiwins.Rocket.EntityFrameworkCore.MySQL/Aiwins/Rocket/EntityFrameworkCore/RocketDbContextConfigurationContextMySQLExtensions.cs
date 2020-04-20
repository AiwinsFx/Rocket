using System;
using Aiwins.Rocket.EntityFrameworkCore.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public static class RocketDbContextConfigurationContextMySQLExtensions {
        public static DbContextOptionsBuilder UseMySQL (
            [NotNull] this RocketDbContextConfigurationContext context, [CanBeNull] Action<MySqlDbContextOptionsBuilder> mySQLOptionsAction = null) {
            if (context.ExistingConnection != null) {
                return context.DbContextOptions.UseMySql (context.ExistingConnection, mySQLOptionsAction);
            } else {
                return context.DbContextOptions.UseMySql (context.ConnectionString, mySQLOptionsAction);
            }
        }
    }
}