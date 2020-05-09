using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Docs
{
    [DependsOn(
        typeof(DocsDomainModule),
        typeof(DocsApplicationContractsModule),
        typeof(RocketCachingModule),
        typeof(RocketAutoMapperModule))]
    public class DocsApplicationModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DocsApplicationModule>();
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<DocsApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}
