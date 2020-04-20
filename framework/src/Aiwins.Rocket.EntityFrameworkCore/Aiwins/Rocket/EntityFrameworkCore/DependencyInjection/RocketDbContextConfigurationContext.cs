using System;
using System.Data.Common;
using Aiwins.Rocket.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aiwins.Rocket.EntityFrameworkCore.DependencyInjection {
    public class RocketDbContextConfigurationContext : IServiceProviderAccessor {
        public IServiceProvider ServiceProvider { get; }

        public string ConnectionString { get; }

        public string ConnectionStringName { get; }

        public DbConnection ExistingConnection { get; }

        public DbContextOptionsBuilder DbContextOptions { get; protected set; }

        public RocketDbContextConfigurationContext (
            [NotNull] string connectionString, [NotNull] IServiceProvider serviceProvider, [CanBeNull] string connectionStringName, [CanBeNull] DbConnection existingConnection) {
            ConnectionString = connectionString;
            ServiceProvider = serviceProvider;
            ConnectionStringName = connectionStringName;
            ExistingConnection = existingConnection;

            DbContextOptions = new DbContextOptionsBuilder ()
                .UseLoggerFactory (serviceProvider.GetRequiredService<ILoggerFactory> ());
        }
    }

    public class RocketDbContextConfigurationContext<TDbContext> : RocketDbContextConfigurationContext
    where TDbContext : RocketDbContext<TDbContext> {
        public new DbContextOptionsBuilder<TDbContext> DbContextOptions => (DbContextOptionsBuilder<TDbContext>) base.DbContextOptions;

        public RocketDbContextConfigurationContext (
            string connectionString, [NotNull] IServiceProvider serviceProvider, [CanBeNull] string connectionStringName, [CanBeNull] DbConnection existingConnection) : base (
            connectionString,
            serviceProvider,
            connectionStringName,
            existingConnection) {
            base.DbContextOptions = new DbContextOptionsBuilder<TDbContext> ()
                .UseLoggerFactory (serviceProvider.GetRequiredService<ILoggerFactory> ());
        }
    }
}