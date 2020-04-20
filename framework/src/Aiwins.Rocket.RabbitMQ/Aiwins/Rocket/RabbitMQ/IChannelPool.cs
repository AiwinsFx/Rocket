using System;

namespace Aiwins.Rocket.RabbitMQ {
    public interface IChannelPool : IDisposable {
        IChannelAccessor Acquire (string channelName = null, string connectionName = null);
    }
}