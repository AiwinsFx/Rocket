using System.Collections.Generic;

namespace Aiwins.Rocket.Http.Client {
    public class RemoteServiceConfiguration : Dictionary<string, string> {
        /// <summary>
        /// 基地址
        /// </summary>
        public string BaseUrl {
            get => this.GetOrDefault (nameof (BaseUrl));
            set => this [nameof (BaseUrl)] = value;
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version {
            get => this.GetOrDefault (nameof (Version));
            set => this [nameof (Version)] = value;
        }

        public RemoteServiceConfiguration () {

        }

        public RemoteServiceConfiguration (string baseUrl, string version = null) {
            this [nameof (BaseUrl)] = baseUrl;
            this [nameof (Version)] = version;
        }
    }
}