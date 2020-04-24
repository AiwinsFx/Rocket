using System;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.BackgroundJobs.MongoDB
{
    public static class BackgroundJobsMongoDbContextExtensions
    {
        public static void ConfigureBackgroundJobs(
            this IMongoModelBuilder builder,
            Action<RocketMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new BackgroundJobsMongoModelBuilderConfigurationOptions(
                BackgroundJobsDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);

            builder.Entity<BackgroundJobRecord>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "BackgroundJobs";
            });
        }
    }
}