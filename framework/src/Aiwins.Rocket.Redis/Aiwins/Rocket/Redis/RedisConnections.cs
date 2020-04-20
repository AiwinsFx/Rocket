using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using StackExchange.Redis;

namespace Aiwins.Rocket.Redis {
    [Serializable]
    public class RedisConnections : Dictionary<string, ConnectionMultiplexer> {
        public const string DefaultConnectionName = "Default";

        [NotNull]
        public ConnectionMultiplexer Default {
            get => this [DefaultConnectionName];
            set => this [DefaultConnectionName] = Check.NotNull (value, nameof (value));
        }

        public RedisConnections () {
            Default = new ConnectionMultiplexer ();
        }

        public ConnectionMultiplexer GetOrDefault (string connectionName) {
            if (TryGetValue (connectionName, out var connectionMultiplexer)) {
                return connectionMultiplexer;
            }

            return Default;
        }
    }
}