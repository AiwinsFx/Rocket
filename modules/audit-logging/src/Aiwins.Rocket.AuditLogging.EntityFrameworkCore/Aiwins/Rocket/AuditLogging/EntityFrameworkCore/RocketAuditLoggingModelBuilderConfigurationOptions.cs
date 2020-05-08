using Aiwins.Rocket.EntityFrameworkCore.Modeling;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Identity.EntityFrameworkCore {
    public class RocketAuditLoggingModelBuilderConfigurationOptions : RocketModelBuilderConfigurationOptions {
        public RocketAuditLoggingModelBuilderConfigurationOptions (
            [NotNull] string tablePrefix, [CanBeNull] string schema) : base (
            tablePrefix,
            schema) {

        }
    }
}