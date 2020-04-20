using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using StackExchange.Redis;

namespace Aiwins.Rocket.Redis {
    [Serializable]
    public class RedisConnections : Dictionary<string, string> {
        public const string DefaultConnectionName = "Default";

        [NotNull]
        public string Default {
            get => this [DefaultConnectionName];
            set => this [DefaultConnectionName] = Check.NotNull (value, nameof (value));
        }

        public RedisConnections () {
            Default = string.Empty;
        }

        public string GetOrDefault (string connectionName) {
            if (TryGetValue (connectionName, out var connection)) {
                return connection;
            }

            return Default;
        }
    }
}