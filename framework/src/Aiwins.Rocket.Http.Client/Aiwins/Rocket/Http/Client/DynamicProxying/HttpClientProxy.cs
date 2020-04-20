namespace Aiwins.Rocket.Http.Client.DynamicProxying {
    public class HttpClientProxy<TRemoteService> : IHttpClientProxy<TRemoteService> {
        public TRemoteService Service { get; }

        public HttpClientProxy (TRemoteService service) {
            Service = service;
        }
    }
}