using Aiwins.Rocket.MongoDB;
using JetBrains.Annotations;

namespace Aiwins.Rocket.FeatureManagement.MongoDB {
    public class FeatureManagementMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions {
        public FeatureManagementMongoModelBuilderConfigurationOptions (
            [NotNull] string collectionPrefix = "") : base (collectionPrefix) {

        }
    }
}