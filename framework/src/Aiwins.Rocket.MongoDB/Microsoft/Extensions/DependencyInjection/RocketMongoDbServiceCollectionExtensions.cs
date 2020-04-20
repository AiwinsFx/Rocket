using System;
using Aiwins.Rocket.MongoDB;
using Aiwins.Rocket.MongoDB.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection {
    public static class RocketMongoDbServiceCollectionExtensions {
        public static IServiceCollection AddMongoDbContext<TMongoDbContext> (this IServiceCollection services, Action<IRocketMongoDbContextRegistrationOptionsBuilder> optionsBuilder = null) //Created overload instead of default parameter
        where TMongoDbContext : RocketMongoDbContext {
            var options = new RocketMongoDbContextRegistrationOptions (typeof (TMongoDbContext), services);
            optionsBuilder?.Invoke (options);

            foreach (var dbContextType in options.ReplacedDbContextTypes) {
                services.Replace (ServiceDescriptor.Transient (dbContextType, typeof (TMongoDbContext)));
            }

            new MongoDbRepositoryRegistrar (options).AddRepositories ();

            return services;
        }
    }
}