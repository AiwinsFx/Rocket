using System;
using StackExchange.Redis;

namespace Aiwins.Rocket.Redis {
    public interface IConnectionMultiplexerPool : IDisposable {
        IConnectionMultiplexer Get (string connectionName = null);
    }
}