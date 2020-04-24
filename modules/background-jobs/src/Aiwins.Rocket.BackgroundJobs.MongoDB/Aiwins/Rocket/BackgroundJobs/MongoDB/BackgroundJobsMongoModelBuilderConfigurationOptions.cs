using JetBrains.Annotations;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.BackgroundJobs.MongoDB
{
    public class BackgroundJobsMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions
    {
        public BackgroundJobsMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}