using System;
using System.Collections.Generic;
using Aiwins.Rocket.Http.Client.DynamicProxying;

namespace Aiwins.Rocket.Http.Client {
    public class RocketHttpClientOptions {
        public Dictionary<Type, DynamicHttpClientProxyConfig> HttpClientProxies { get; set; }

        public RocketHttpClientOptions () {
            HttpClientProxies = new Dictionary<Type, DynamicHttpClientProxyConfig> ();
        }
    }
}