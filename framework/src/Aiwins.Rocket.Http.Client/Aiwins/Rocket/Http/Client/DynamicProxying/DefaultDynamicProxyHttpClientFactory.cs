using System.Net.Http;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Http.Client.DynamicProxying {
    public class DefaultDynamicProxyHttpClientFactory : IDynamicProxyHttpClientFactory, ITransientDependency {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultDynamicProxyHttpClientFactory (IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient Create () {
            return _httpClientFactory.CreateClient ();
        }

        public HttpClient Create (string name) {
            return _httpClientFactory.CreateClient (name);
        }
    }
}