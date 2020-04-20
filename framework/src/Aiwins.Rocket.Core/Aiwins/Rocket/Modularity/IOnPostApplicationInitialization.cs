using JetBrains.Annotations;

namespace Aiwins.Rocket.Modularity {
    public interface IOnPostApplicationInitialization {
        void OnPostApplicationInitialization ([NotNull] ApplicationInitializationContext context);
    }
}