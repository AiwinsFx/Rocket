using JetBrains.Annotations;

namespace Aiwins.Rocket.Tracing {
    public interface ICorrelationIdProvider {
        [NotNull]
        string Get ();
    }
}