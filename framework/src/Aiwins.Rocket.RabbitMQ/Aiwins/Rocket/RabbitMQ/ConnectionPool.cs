using System.Collections.Concurrent;
using System.Collections.Generic;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Aiwins.Rocket.RabbitMQ {
    public class ConnectionPool : IConnectionPool, ISingletonDependency {
        protected RocketRabbitMqOptions Options { get; }

        protected ConcurrentDictionary<string, IConnection> Connections { get; }

        private bool _isDisposed;

        public ConnectionPool (IOptions<RocketRabbitMqOptions> options) {
            Options = options.Value;
            Connections = new ConcurrentDictionary<string, IConnection> ();
        }

        public virtual IConnection Get (string connectionName = null) {
            connectionName = connectionName ??
                RabbitMqConnections.DefaultConnectionName;

            return Connections.GetOrAdd (
                connectionName,
                () => Options
                .Connections
                .GetOrDefault (connectionName)
                .CreateConnection ()
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