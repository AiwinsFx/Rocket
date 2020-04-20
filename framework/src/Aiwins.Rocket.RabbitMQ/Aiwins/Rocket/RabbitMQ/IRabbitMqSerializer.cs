using System;

namespace Aiwins.Rocket.RabbitMQ {
    public interface IRabbitMqSerializer {
        byte[] Serialize (object obj);

        object Deserialize (byte[] value, Type type);
    }
}