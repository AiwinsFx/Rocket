using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Blogging.EntityFrameworkCore
{
    [DependsOn(
        typeof(BloggingDomainModule),
        typeof(RocketEntityFrameworkCoreModule))]
    public class BloggingEntityFrameworkCoreModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddRocketDbContext<BloggingDbContext>();
        }
    }
}
