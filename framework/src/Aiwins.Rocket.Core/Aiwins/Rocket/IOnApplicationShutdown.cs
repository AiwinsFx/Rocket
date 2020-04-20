using JetBrains.Annotations;

namespace Aiwins.Rocket {
    public interface IOnApplicationShutdown {
        void OnApplicationShutdown ([NotNull] ApplicationShutdownContext context);
    }
}