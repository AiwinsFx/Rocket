using System;
using Aiwins.Rocket.Modularity;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket {
    public static class RocketApplicationFactory {
        public static IRocketApplicationWithInternalServiceProvider Create<TStartupModule> (
            [CanBeNull] Action<RocketApplicationCreationOptions> optionsAction = null)
        where TStartupModule : IRocketModule {
            return Create (typeof (TStartupModule), optionsAction);
        }

        public static IRocketApplicationWithInternalServiceProvider Create (
            [NotNull] Type startupModuleType, [CanBeNull] Action<RocketApplicationCreationOptions> optionsAction = null) {
            return new RocketApplicationWithInternalServiceProvider (startupModuleType, optionsAction);
        }

        public static IRocketApplicationWithExternalServiceProvider Create<TStartupModule> (
            [NotNull] IServiceCollection services, [CanBeNull] Action<RocketApplicationCreationOptions> optionsAction = null)
        where TStartupModule : IRocketModule {
            return Create (typeof (TStartupModule), services, optionsAction);
        }

        public static IRocketApplicationWithExternalServiceProvider Create (
            [NotNull] Type startupModuleType, [NotNull] IServiceCollection services, [CanBeNull] Action<RocketApplicationCreationOptions> optionsAction = null) {
            return new RocketApplicationWithExternalServiceProvider (startupModuleType, services, optionsAction);
        }
    }
}