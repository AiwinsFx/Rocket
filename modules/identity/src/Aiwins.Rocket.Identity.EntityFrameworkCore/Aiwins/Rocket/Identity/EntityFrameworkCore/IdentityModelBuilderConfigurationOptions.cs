using Aiwins.Rocket.EntityFrameworkCore.Modeling;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Identity.EntityFrameworkCore {
    public class IdentityModelBuilderConfigurationOptions : RocketModelBuilderConfigurationOptions {
        public IdentityModelBuilderConfigurationOptions (
            [NotNull] string tablePrefix, [CanBeNull] string schema) : base (
            tablePrefix,
            schema) {

        }
    }
}