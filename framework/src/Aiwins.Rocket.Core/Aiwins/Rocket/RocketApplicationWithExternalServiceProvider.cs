using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket {
    internal class RocketApplicationWithExternalServiceProvider : RocketApplicationBase, IRocketApplicationWithExternalServiceProvider {
        public RocketApplicationWithExternalServiceProvider (
            [NotNull] Type startupModuleType, [NotNull] IServiceCollection services, [CanBeNull] Action<RocketApplicationCreationOptions> optionsAction
        ) : base (
            startupModuleType,
            services,
            optionsAction) {
            services.AddSingleton<IRocketApplicationWithExternalServiceProvider> (this);
        }

        public void Initialize (IServiceProvider serviceProvider) {
            Check.NotNull (serviceProvider, nameof (serviceProvider));

            SetServiceProvider (serviceProvider);

            InitializeModules ();
        }

        public override void Dispose () {
            base.Dispose ();

            if (ServiceProvider is IDisposable disposableServiceProvider) {
                disposableServiceProvider.Dispose ();
            }
        }
    }
}