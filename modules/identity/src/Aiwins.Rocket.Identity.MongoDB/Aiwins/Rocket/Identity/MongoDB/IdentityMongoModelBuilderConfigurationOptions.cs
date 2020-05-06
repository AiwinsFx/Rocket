using Aiwins.Rocket.MongoDB;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Identity.MongoDB {
    public class IdentityMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions {
        public IdentityMongoModelBuilderConfigurationOptions ([NotNull] string collectionPrefix = "") : base (collectionPrefix) { }
    }
}