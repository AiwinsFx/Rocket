using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Docs
{
    [DependsOn(
        typeof(DocsApplicationContractsModule),
        typeof(RocketAspNetCoreMvcModule)
        )]
    public class DocsHttpApiModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DocsHttpApiModule).Assembly);
            });
        }
    }
}
