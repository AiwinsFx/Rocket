using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Penguin.Core.Extensions;
using Penguin.Core.Utility;
using StackExchange.Redis;

namespace Penguin.Insulation.Redis {
    public sealed class RedisConnectionMapping : IConnectionMapping {
        private const string KeyPrefix = "Hub:";
        private readonly ConnectionMultiplexer _connection;

        public RedisConnectionMapping (ConnectionMultiplexer connection) {
            _connection = connection;
        }

        public Task AddAsync (string key, string connectionId) {
            if (key == null)
                return Task.CompletedTask;

            return Database.SetAddAsync (String.Concat (KeyPrefix, key), connectionId);
        }

        private IDatabase Database => _connection.GetDatabase ();

        public async Task<ICollection<string>> GetConnectionsAsync (string key) {
            if (key == null)
                return new List<string> ();

            var values = await Database.SetMembersAsync (String.Concat (KeyPrefix, key));
            return values.Select (v => v.ToString ()).ToList ();
        }

        public async Task<int> GetConnectionCountAsync (string key) {
            if (key == null)
                return 0;

            return (int) await Database.SetLengthAsync (String.Concat (KeyPrefix, key));
        }

        public Task RemoveAsync (string key, string connectionId) {
            if (key == null)
                return Task.CompletedTask;

            return Database.SetRemoveAsync (String.Concat (KeyPrefix, key), connectionId);
        }
    }
}