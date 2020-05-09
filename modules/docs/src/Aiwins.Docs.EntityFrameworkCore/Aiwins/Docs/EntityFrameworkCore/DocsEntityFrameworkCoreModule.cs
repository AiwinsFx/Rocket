using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;
using Aiwins.Docs.Documents;
using Aiwins.Docs.Projects;

namespace Aiwins.Docs.EntityFrameworkCore
{
    [DependsOn(
        typeof(DocsDomainModule),
        typeof(RocketEntityFrameworkCoreModule))]
    public class DocsEntityFrameworkCoreModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddRocketDbContext<DocsDbContext>(options =>
            {
                options.AddRepository<Project, EfCoreProjectRepository>();
                options.AddRepository<Document, EFCoreDocumentRepository>();
            });
        }
    }
}
