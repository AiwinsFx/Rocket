using System;
using System.Text;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Json;

namespace Aiwins.Rocket.Caching {
    public class Utf8JsonDistributedCacheSerializer : IDistributedCacheSerializer, ITransientDependency {
        protected IJsonSerializer JsonSerializer { get; }

        public Utf8JsonDistributedCacheSerializer (IJsonSerializer jsonSerializer) {
            JsonSerializer = jsonSerializer;
        }

        public byte[] Serialize<T> (T obj) {
            return Encoding.UTF8.GetBytes (JsonSerializer.Serialize (obj));
        }

        public T Deserialize<T> (byte[] bytes) {
            return (T) JsonSerializer.Deserialize (typeof (T), Encoding.UTF8.GetString (bytes));
        }

        public byte[] Serialize (object obj) {
            return Encoding.UTF8.GetBytes (JsonSerializer.Serialize (obj));
        }

        public object Deserialize (byte[] bytes, Type type) {
            return JsonSerializer.Deserialize (type, Encoding.UTF8.GetString (bytes));
        }
    }
}