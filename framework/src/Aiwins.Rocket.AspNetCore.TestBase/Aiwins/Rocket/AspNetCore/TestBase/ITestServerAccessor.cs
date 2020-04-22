using Microsoft.AspNetCore.TestHost;

namespace Aiwins.Rocket.AspNetCore.TestBase
{
    public interface ITestServerAccessor 
    {
        TestServer Server { get; set; }
    }
}