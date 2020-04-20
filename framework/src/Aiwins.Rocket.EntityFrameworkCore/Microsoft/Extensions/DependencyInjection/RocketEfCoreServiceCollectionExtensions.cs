using System;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection {
    public static class RocketEfCoreServiceCollectionExtensions {
        public static IServiceCollection AddRocketDbContext<TDbContext> (
            this IServiceCollection services,
            Action<IRocketDbContextRegistrationOptionsBuilder> optionsBuilder = null)
        where TDbContext : RocketDbContext<TDbContext> {
            services.AddMemoryCache ();

            var options = new RocketDbContextRegistrationOptions (typeof (TDbContext), services);
            optionsBuilder?.Invoke (options);

            services.TryAddTransient (DbContextOptionsFactory.Create<TDbContext>);

            foreach (var dbContextType in options.ReplacedDbContextTypes) {
                services.Replace (ServiceDescriptor.Transient (dbContextType, typeof (TDbContext)));
            }

            new EfCoreRepositoryRegistrar (options).AddRepositories ();

            return services;
        }
    }
}