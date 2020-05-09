using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;
using Aiwins.Docs.Projects;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Docs.Documents;

namespace Aiwins.Docs.MongoDB
{
    [DependsOn(
        typeof(DocsDomainModule),
        typeof(RocketMongoDbModule)
    )]
    public class DocsMongoDbModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<DocsMongoDbContext>(options =>
            {
                options.AddRepository<Project, MongoProjectRepository>();
                options.AddRepository<Document, MongoDocumentRepository>();
            });
        }
    }
}
