using JetBrains.Annotations;
using Aiwins.Rocket.EntityFrameworkCore.Modeling;

namespace Aiwins.Rocket.Identity.EntityFrameworkCore
{
    public class RocketAuditLoggingModelBuilderConfigurationOptions : RocketModelBuilderConfigurationOptions
    {
        public RocketAuditLoggingModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix,
            [CanBeNull] string schema)
            : base(
                tablePrefix, 
                schema)
        {

        }
    }
}