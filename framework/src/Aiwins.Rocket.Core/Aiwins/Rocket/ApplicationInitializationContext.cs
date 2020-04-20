using System;
using Aiwins.Rocket.DependencyInjection;
using JetBrains.Annotations;

namespace Aiwins.Rocket {
    public class ApplicationInitializationContext : IServiceProviderAccessor {
        public IServiceProvider ServiceProvider { get; set; }

        public ApplicationInitializationContext ([NotNull] IServiceProvider serviceProvider) {
            Check.NotNull (serviceProvider, nameof (serviceProvider));

            ServiceProvider = serviceProvider;
        }
    }
}