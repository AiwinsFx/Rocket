using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Aiwins.Rocket.EntityFrameworkCore {
    public static class RocketDbContextOptionsMySQLExtensions {
        public static void UseMySQL (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<MySqlDbContextOptionsBuilder> mySQLOptionsAction = null) {
            options.Configure (context => {
                context.UseMySQL (mySQLOptionsAction);
            });
        }

        public static void UseMySQL<TDbContext> (
            [NotNull] this RocketDbContextOptions options, [CanBeNull] Action<MySqlDbContextOptionsBuilder> mySQLOptionsAction = null)
        where TDbContext : RocketDbContext<TDbContext> {
            options.Configure<TDbContext> (context => {
                context.UseMySQL (mySQLOptionsAction);
            });
        }
    }
}