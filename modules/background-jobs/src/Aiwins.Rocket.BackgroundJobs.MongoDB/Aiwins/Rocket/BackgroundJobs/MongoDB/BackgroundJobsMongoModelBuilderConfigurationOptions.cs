using Aiwins.Rocket.MongoDB;
using JetBrains.Annotations;

namespace Aiwins.Rocket.BackgroundJobs.MongoDB {
    public class BackgroundJobsMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions {
        public BackgroundJobsMongoModelBuilderConfigurationOptions (
            [NotNull] string collectionPrefix = "") : base (collectionPrefix) { }
    }
}