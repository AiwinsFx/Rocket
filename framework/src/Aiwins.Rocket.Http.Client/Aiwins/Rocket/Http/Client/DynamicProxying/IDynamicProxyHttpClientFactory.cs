using System.Net.Http;

namespace Aiwins.Rocket.Http.Client.DynamicProxying {
    public interface IDynamicProxyHttpClientFactory {
        HttpClient Create ();

        HttpClient Create (string name);
    }
}