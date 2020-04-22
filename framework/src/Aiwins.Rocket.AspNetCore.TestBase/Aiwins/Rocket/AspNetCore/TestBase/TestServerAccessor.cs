using Microsoft.AspNetCore.TestHost;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.TestBase
{
    public class TestServerAccessor : ITestServerAccessor, ISingletonDependency
    {
        public TestServer Server { get; set; }
    }
}