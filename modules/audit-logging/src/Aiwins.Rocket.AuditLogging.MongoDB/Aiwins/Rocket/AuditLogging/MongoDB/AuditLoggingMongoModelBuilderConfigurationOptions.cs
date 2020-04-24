using JetBrains.Annotations;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.AuditLogging.MongoDB
{
    public class AuditLoggingMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions
    {
        public AuditLoggingMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}
