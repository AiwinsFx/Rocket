using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aiwins.Rocket.Internal {
    internal static class InternalServiceCollectionExtensions {
        internal static void AddCoreServices (this IServiceCollection services) {
            services.AddOptions ();
            services.AddLogging ();
            services.AddLocalization ();
        }

        internal static void AddCoreRocketServices (this IServiceCollection services,
            IRocketApplication rocketApplication,
            RocketApplicationCreationOptions applicationCreationOptions) {
            var moduleLoader = new ModuleLoader ();
            var assemblyFinder = new AssemblyFinder (rocketApplication);
            var typeFinder = new TypeFinder (assemblyFinder);

            if (!services.IsAdded<IConfiguration> ()) {
                services.ReplaceConfiguration (
                    ConfigurationHelper.BuildConfiguration (
                        applicationCreationOptions.Configuration
                    )
                );
            }

            services.TryAddSingleton<IModuleLoader> (moduleLoader);
            services.TryAddSingleton<IAssemblyFinder> (assemblyFinder);
            services.TryAddSingleton<ITypeFinder> (typeFinder);

            services.AddAssemblyOf<IRocketApplication> ();

            services.Configure<RocketModuleLifecycleOptions> (options => {
                options.Contributors.Add<OnPreApplicationInitializationModuleLifecycleContributor> ();
                options.Contributors.Add<OnApplicationInitializationModuleLifecycleContributor> ();
                options.Contributors.Add<OnPostApplicationInitializationModuleLifecycleContributor> ();
                options.Contributors.Add<OnApplicationShutdownModuleLifecycleContributor> ();
            });
        }
    }
}