using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Aiwins.Rocket.Auditing {
    //TODO: 考虑重命名为JsonAuditSerializer
    public class JsonNetAuditSerializer : IAuditSerializer, ITransientDependency {
        protected RocketAuditingOptions Options;

        public JsonNetAuditSerializer (IOptions<RocketAuditingOptions> options) {
            Options = options.Value;
        }

        public string Serialize (object obj) {
            return JsonConvert.SerializeObject (obj, GetSharedJsonSerializerSettings ());
        }

        private static readonly object SyncObj = new object ();
        private static JsonSerializerSettings _sharedJsonSerializerSettings;

        private JsonSerializerSettings GetSharedJsonSerializerSettings () {
            if (_sharedJsonSerializerSettings == null) {
                lock (SyncObj) {
                if (_sharedJsonSerializerSettings == null) {
                _sharedJsonSerializerSettings = new JsonSerializerSettings {
                ContractResolver = new AuditingContractResolver (Options.IgnoredTypes)
                        };
                    }
                }
            }

            return _sharedJsonSerializerSettings;
        }
    }
}