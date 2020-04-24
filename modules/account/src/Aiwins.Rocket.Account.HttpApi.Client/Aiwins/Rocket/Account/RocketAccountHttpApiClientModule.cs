using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Http.Client;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Account
{
    [DependsOn(
        typeof(RocketAccountApplicationContractsModule),
        typeof(RocketHttpClientModule))]
    public class RocketAccountHttpApiClientModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(typeof(RocketAccountApplicationContractsModule).Assembly, 
                AccountRemoteServiceConsts.RemoteServiceName);
        }
    }
}