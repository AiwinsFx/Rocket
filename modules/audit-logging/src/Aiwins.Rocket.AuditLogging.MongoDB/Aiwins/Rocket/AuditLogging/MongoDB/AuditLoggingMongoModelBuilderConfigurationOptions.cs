using Aiwins.Rocket.MongoDB;
using JetBrains.Annotations;

namespace Aiwins.Rocket.AuditLogging.MongoDB {
    public class AuditLoggingMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions {
        public AuditLoggingMongoModelBuilderConfigurationOptions (
            [NotNull] string collectionPrefix = "") : base (collectionPrefix) { }
    }
}