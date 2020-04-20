using System;
using StackExchange.Redis;

namespace Aiwins.Rocket.Redis {
    public interface IConnectionPool : IDisposable {
        IConnection Get (string connectionName = null);
    }
}