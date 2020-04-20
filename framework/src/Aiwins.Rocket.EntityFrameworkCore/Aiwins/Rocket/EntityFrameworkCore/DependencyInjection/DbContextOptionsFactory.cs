using System;
using System.Collections.Generic;
using Aiwins.Rocket.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.EntityFrameworkCore.DependencyInjection {
    internal static class DbContextOptionsFactory {
        public static DbContextOptions<TDbContext> Create<TDbContext> (IServiceProvider serviceProvider)
        where TDbContext : RocketDbContext<TDbContext> {
            var creationContext = GetCreationContext<TDbContext> (serviceProvider);

            var context = new RocketDbContextConfigurationContext<TDbContext> (
                creationContext.ConnectionString,
                serviceProvider,
                creationContext.ConnectionStringName,
                creationContext.ExistingConnection
            );

            var options = GetDbContextOptions<TDbContext> (serviceProvider);

            PreConfigure (options, context);
            Configure (options, context);

            return context.DbContextOptions.Options;
        }

        private static void PreConfigure<TDbContext> (
            RocketDbContextOptions options,
            RocketDbContextConfigurationContext<TDbContext> context)
        where TDbContext : RocketDbContext<TDbContext> {
            foreach (var defaultPreConfigureAction in options.DefaultPreConfigureActions) {
                defaultPreConfigureAction.Invoke (context);
            }

            var preConfigureActions = options.PreConfigureActions.GetOrDefault (typeof (TDbContext));
            if (!preConfigureActions.IsNullOrEmpty ()) {
                foreach (var preConfigureAction in preConfigureActions) {
                    ((Action<RocketDbContextConfigurationContext<TDbContext>>) preConfigureAction).Invoke (context);
                }
            }
        }

        private static void Configure<TDbContext> (
            RocketDbContextOptions options,
            RocketDbContextConfigurationContext<TDbContext> context)
        where TDbContext : RocketDbContext<TDbContext> {
            var configureAction = options.ConfigureActions.GetOrDefault (typeof (TDbContext));
            if (configureAction != null) {
                ((Action<RocketDbContextConfigurationContext<TDbContext>>) configureAction).Invoke (context);
            } else if (options.DefaultConfigureAction != null) {
                options.DefaultConfigureAction.Invoke (context);
            } else {
                throw new RocketException (
                    $"No configuration found for {typeof(DbContext).AssemblyQualifiedName}! Use services.Configure<RocketDbContextOptions>(...) to configure it.");
            }
        }

        private static RocketDbContextOptions GetDbContextOptions<TDbContext> (IServiceProvider serviceProvider)
        where TDbContext : RocketDbContext<TDbContext> {
            return serviceProvider.GetRequiredService<IOptions<RocketDbContextOptions>> ().Value;
        }

        private static DbContextCreationContext GetCreationContext<TDbContext> (IServiceProvider serviceProvider)
        where TDbContext : RocketDbContext<TDbContext> {
            var context = DbContextCreationContext.Current;
            if (context != null) {
                return context;
            }

            var connectionStringName = ConnectionStringNameAttribute.GetConnStringName<TDbContext> ();
            var connectionString = serviceProvider.GetRequiredService<IConnectionStringResolver> ().Resolve (connectionStringName);

            return new DbContextCreationContext (
                connectionStringName,
                connectionString
            );
        }
    }
}