using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Http.Client;
using Aiwins.Rocket.Modularity;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameApplicationContractsModule),
        typeof(RocketHttpClientModule))]
    public class MyProjectNameHttpApiClientModule : RocketModule
    {
        public const string RemoteServiceName = "MyProjectName";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(MyProjectNameApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
