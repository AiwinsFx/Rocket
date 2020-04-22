using Aiwins.Rocket.DependencyInjection;
using Microsoft.AspNetCore.TestHost;

namespace Aiwins.Rocket.AspNetCore.TestBase {
    public class TestServerAccessor : ITestServerAccessor, ISingletonDependency {
        public TestServer Server { get; set; }
    }
}