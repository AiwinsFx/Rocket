using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Http.Client;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Blogging
{
    [DependsOn(
        typeof(BloggingApplicationContractsModule),
        typeof(RocketHttpClientModule))]
    public class BloggingHttpApiClientModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(typeof(BloggingApplicationContractsModule).Assembly, 
                BloggingRemoteServiceConsts.RemoteServiceName);
        }

    }
}
