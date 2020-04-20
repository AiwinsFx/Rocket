using System;

namespace Aiwins.Rocket.Redis {
    public interface IRedisSerializer {
        byte[] Serialize (object obj);

        object Deserialize (byte[] value, Type type);
    }
}