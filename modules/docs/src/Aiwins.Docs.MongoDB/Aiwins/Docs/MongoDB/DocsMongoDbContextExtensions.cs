using System;
using Aiwins.Rocket;
using Aiwins.Rocket.MongoDB;
using Aiwins.Docs.Documents;
using Aiwins.Docs.Projects;

namespace Aiwins.Docs.MongoDB
{
    public static class DocsMongoDbContextExtensions
    {
        public static void ConfigureDocs(
            this IMongoModelBuilder builder,
            Action<RocketMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DocsMongoModelBuilderConfigurationOptions(
                DocsDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);

            builder.Entity<Project>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "Projects";
            });

            builder.Entity<Document>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "DocumentS";
            });
        }
    }
}

