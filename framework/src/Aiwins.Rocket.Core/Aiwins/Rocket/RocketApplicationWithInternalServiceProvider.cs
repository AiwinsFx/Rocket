using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket {
    internal class RocketApplicationWithInternalServiceProvider : RocketApplicationBase, IRocketApplicationWithInternalServiceProvider {
        public IServiceScope ServiceScope { get; private set; }

        public RocketApplicationWithInternalServiceProvider (
            [NotNull] Type startupModuleType, [CanBeNull] Action<RocketApplicationCreationOptions> optionsAction
        ) : this (
            startupModuleType,
            new ServiceCollection (),
            optionsAction) {

        }

        private RocketApplicationWithInternalServiceProvider (
            [NotNull] Type startupModuleType, [NotNull] IServiceCollection services, [CanBeNull] Action<RocketApplicationCreationOptions> optionsAction
        ) : base (
            startupModuleType,
            services,
            optionsAction) {
            Services.AddSingleton<IRocketApplicationWithInternalServiceProvider> (this);
        }

        public void Initialize () {
            ServiceScope = Services.BuildServiceProviderFromFactory ().CreateScope ();
            SetServiceProvider (ServiceScope.ServiceProvider);

            InitializeModules ();
        }

        public override void Dispose () {
            base.Dispose ();
            ServiceScope.Dispose ();
        }
    }
}