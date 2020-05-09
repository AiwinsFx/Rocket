using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Aiwins.Rocket;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.EventBus.Distributed;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Threading;
using Aiwins.Rocket.VirtualFileSystem;
using Aiwins.Docs.Documents;
using Aiwins.Docs.Documents.FullSearch.Elastic;
using Aiwins.Docs.FileSystem.Documents;
using Aiwins.Docs.GitHub.Documents;
using Aiwins.Docs.Localization;
using Aiwins.Docs.Projects;

namespace Aiwins.Docs
{
    [DependsOn(
        typeof(DocsDomainSharedModule),
        typeof(RocketDddDomainModule),
        typeof(RocketAutoMapperModule)
        )]
    public class DocsDomainModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DocsDomainModule>();

            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<DocsDomainMappingProfile>(validate: true);
            });

            Configure<RocketDistributedEventBusOptions>(options =>
            {
                options.EtoMappings.Add<Document, DocumentEto>(typeof(DocsDomainModule));
                options.EtoMappings.Add<Project, ProjectEto>(typeof(DocsDomainModule));
            });
            
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets
                    .AddEmbedded<DocsDomainModule>();
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DocsResource>()
                    .AddVirtualJson("/Aiwins/Docs/Localization/Domain");
            });

            Configure<DocumentSourceOptions>(options =>
            {
                options.Sources[GithubDocumentSource.Type] = typeof(GithubDocumentSource);
                options.Sources[FileSystemDocumentSource.Type] = typeof(FileSystemDocumentSource);
            });

            context.Services.AddHttpClient(GithubRepositoryManager.HttpClientName, client =>
            {
                client.Timeout = TimeSpan.FromMilliseconds(15000);
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            using (var scope = context.ServiceProvider.CreateScope())
            {
                if (scope.ServiceProvider.GetRequiredService<IOptions<DocsElasticSearchOptions>>().Value.Enable)
                {
                    var documentFullSearch = scope.ServiceProvider.GetRequiredService<IDocumentFullSearch>();
                    AsyncHelper.RunSync(() => documentFullSearch.CreateIndexIfNeededAsync());
                }
            }
        }
    }
}
