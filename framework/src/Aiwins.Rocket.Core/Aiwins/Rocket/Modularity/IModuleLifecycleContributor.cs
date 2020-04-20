using Aiwins.Rocket.DependencyInjection;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Modularity {
    public interface IModuleLifecycleContributor : ITransientDependency {
        void Initialize ([NotNull] ApplicationInitializationContext context, [NotNull] IRocketModule module);

        void Shutdown ([NotNull] ApplicationShutdownContext context, [NotNull] IRocketModule module);
    }
}