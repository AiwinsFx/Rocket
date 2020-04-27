using System;

namespace Aiwins.Rocket.Caching {
    public interface IDistributedCacheSerializer {
        byte[] Serialize<T> (T obj);

        T Deserialize<T> (byte[] bytes);

        byte[] Serialize (object obj);

        object Deserialize (byte[] bytes, Type type);
    }
}