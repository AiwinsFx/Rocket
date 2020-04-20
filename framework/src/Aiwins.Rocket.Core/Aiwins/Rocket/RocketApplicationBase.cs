using System;
using System.Collections.Generic;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Internal;
using Aiwins.Rocket.Modularity;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket {
    public abstract class RocketApplicationBase : IRocketApplication {
        [NotNull]
        public Type StartupModuleType { get; }

        public IServiceProvider ServiceProvider { get; private set; }

        public IServiceCollection Services { get; }

        public IReadOnlyList<IRocketModuleDescriptor> Modules { get; }

        internal RocketApplicationBase (
            [NotNull] Type startupModuleType, [NotNull] IServiceCollection services, [CanBeNull] Action<RocketApplicationCreationOptions> optionsAction) {
            Check.NotNull (startupModuleType, nameof (startupModuleType));
            Check.NotNull (services, nameof (services));

            StartupModuleType = startupModuleType;
            Services = services;

            services.TryAddObjectAccessor<IServiceProvider> ();

            var options = new RocketApplicationCreationOptions (services);
            optionsAction?.Invoke (options);

            services.AddSingleton<IRocketApplication> (this);
            services.AddSingleton<IModuleContainer> (this);

            services.AddCoreServices ();
            services.AddCoreRocketServices (this, options);

            Modules = LoadModules (services, options);
        }

        public virtual void Shutdown () {
            using (var scope = ServiceProvider.CreateScope ()) {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager> ()
                    .ShutdownModules (new ApplicationShutdownContext (scope.ServiceProvider));
            }
        }

        public virtual void Dispose () {
            //TODO: 应用程序释放后考虑做一些额外的操作处理?
        }

        protected virtual void SetServiceProvider (IServiceProvider serviceProvider) {
            ServiceProvider = serviceProvider;
            ServiceProvider.GetRequiredService<ObjectAccessor<IServiceProvider>> ().Value = ServiceProvider;
        }

        protected virtual void InitializeModules () {
            using (var scope = ServiceProvider.CreateScope ()) {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager> ()
                    .InitializeModules (new ApplicationInitializationContext (scope.ServiceProvider));
            }
        }

        private IReadOnlyList<IRocketModuleDescriptor> LoadModules (IServiceCollection services, RocketApplicationCreationOptions options) {
            return services
                .GetSingletonInstance<IModuleLoader> ()
                .LoadModules (
                    services,
                    StartupModuleType,
                    options.PlugInSources
                );
        }
    }
}