using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Docs.Admin
{
    [DependsOn(
        typeof(DocsDomainModule),
        typeof(DocsAdminApplicationContractsModule),
        typeof(RocketCachingModule),
        typeof(RocketAutoMapperModule))]
    public class DocsAdminApplicationModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DocsAdminApplicationModule>();
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<DocsAdminApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}
