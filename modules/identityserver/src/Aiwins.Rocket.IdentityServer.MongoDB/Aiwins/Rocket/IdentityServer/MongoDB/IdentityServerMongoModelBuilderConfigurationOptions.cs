using Aiwins.Rocket.MongoDB;
using JetBrains.Annotations;

namespace Aiwins.Rocket.IdentityServer.MongoDB {
    public class IdentityServerMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions {
        public IdentityServerMongoModelBuilderConfigurationOptions ([NotNull] string collectionPrefix = "") : base (collectionPrefix) { }
    }
}