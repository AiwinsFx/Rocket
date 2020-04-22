using System.Net.Http;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Http.Client.DynamicProxying;

namespace Aiwins.Rocket.AspNetCore.TestBase.DynamicProxying {
    [Dependency (ReplaceServices = true)]
    public class AspNetCoreTestDynamicProxyHttpClientFactory : IDynamicProxyHttpClientFactory, ITransientDependency {
        private readonly ITestServerAccessor _testServerAccessor;

        public AspNetCoreTestDynamicProxyHttpClientFactory (
            ITestServerAccessor testServerAccessor) {
            _testServerAccessor = testServerAccessor;
        }

        public HttpClient Create () {
            return _testServerAccessor.Server.CreateClient ();
        }

        public HttpClient Create (string name) {
            return Create ();
        }
    }
}