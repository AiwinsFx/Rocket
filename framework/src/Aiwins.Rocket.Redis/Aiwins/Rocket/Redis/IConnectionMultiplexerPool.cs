using System;
using StackExchange.Redis;

namespace Aiwins.Rocket.Redis {
    public interface IConnectionPool : IDisposable {
        IConnectionMultiplexer Get (string connectionName = null);
    }
}