using Aiwins.Rocket.EntityFrameworkCore.Modeling;
using JetBrains.Annotations;

namespace Aiwins.Rocket.FeatureManagement.EntityFrameworkCore {
    public class FeatureManagementModelBuilderConfigurationOptions : RocketModelBuilderConfigurationOptions {
        public FeatureManagementModelBuilderConfigurationOptions (
            [NotNull] string tablePrefix = "", [CanBeNull] string schema = null) : base (
            tablePrefix,
            schema) {

        }
    }
}