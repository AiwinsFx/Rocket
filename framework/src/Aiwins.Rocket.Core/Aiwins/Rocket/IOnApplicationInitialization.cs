using JetBrains.Annotations;

namespace Aiwins.Rocket {
    public interface IOnApplicationInitialization {
        void OnApplicationInitialization ([NotNull] ApplicationInitializationContext context);
    }
}