using JetBrains.Annotations;

namespace Aiwins.Rocket.Modularity {
    public interface IModuleManager {
        void InitializeModules ([NotNull] ApplicationInitializationContext context);

        void ShutdownModules ([NotNull] ApplicationShutdownContext context);
    }
}