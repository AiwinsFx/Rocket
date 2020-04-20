using System.Collections.Concurrent;
using System.Collections.Generic;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Aiwins.Rocket.Redis {
    public class ConnectionPool : IConnectionPool, ISingletonDependency {
        protected RocketRedisOptions Options { get; }

        protected ConcurrentDictionary<string, ConnectionMultiplexer> Connections { get; }

        private bool _isDisposed;

        public ConnectionPool (IOptions<RocketRedisOptions> options) {
            Options = options.Value;
            Connections = new ConcurrentDictionary<string, IConnection> ();
        }

        public virtual ConnectionMultiplexer Get (string connectionName = null) {
            connectionName = connectionName ??
                RedisConnections.DefaultConnectionName;

            return Connections.GetOrAdd (
                connectionName,
                () => Options
                .Connections
                .GetOrDefault (connectionName)
                .Connect ()
            );
        }

        public void Dispose () {
            if (_isDisposed) {
                return;
            }

            _isDisposed = true;

            foreach (var connection in Connections.Values) {
                try {
                    connection.Dispose ();
                } catch {

                }
            }

            Connections.Clear ();
        }
    }
}