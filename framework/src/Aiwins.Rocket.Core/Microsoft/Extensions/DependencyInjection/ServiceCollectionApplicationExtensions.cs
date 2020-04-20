using System;
using Aiwins.Rocket;
using Aiwins.Rocket.Modularity;
using JetBrains.Annotations;

namespace Microsoft.Extensions.DependencyInjection {
    public static class ServiceCollectionApplicationExtensions {
        public static IRocketApplicationWithExternalServiceProvider AddApplication<TStartupModule> (
            [NotNull] this IServiceCollection services, [CanBeNull] Action<RocketApplicationCreationOptions> optionsAction = null)
        where TStartupModule : IRocketModule {
            return RocketApplicationFactory.Create<TStartupModule> (services, optionsAction);
        }

        public static IRocketApplicationWithExternalServiceProvider AddApplication (
            [NotNull] this IServiceCollection services, [NotNull] Type startupModuleType, [CanBeNull] Action<RocketApplicationCreationOptions> optionsAction = null) {
            return RocketApplicationFactory.Create (startupModuleType, services, optionsAction);
        }
    }
}