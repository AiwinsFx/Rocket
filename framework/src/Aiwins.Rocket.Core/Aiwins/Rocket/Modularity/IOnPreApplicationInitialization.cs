using JetBrains.Annotations;

namespace Aiwins.Rocket.Modularity {
    public interface IOnPreApplicationInitialization {
        void OnPreApplicationInitialization ([NotNull] ApplicationInitializationContext context);
    }
}