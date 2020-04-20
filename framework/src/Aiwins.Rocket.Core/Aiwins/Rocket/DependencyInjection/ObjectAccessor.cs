using JetBrains.Annotations;

namespace Aiwins.Rocket.DependencyInjection {
    public class ObjectAccessor<T> : IObjectAccessor<T> {
        public T Value { get; set; }

        public ObjectAccessor () {

        }

        public ObjectAccessor ([CanBeNull] T obj) {
            Value = obj;
        }
    }
}