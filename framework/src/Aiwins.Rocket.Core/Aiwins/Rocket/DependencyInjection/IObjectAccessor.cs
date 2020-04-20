using JetBrains.Annotations;

namespace Aiwins.Rocket.DependencyInjection {
    public interface IObjectAccessor<out T> {
        [CanBeNull]
        T Value { get; }
    }
}