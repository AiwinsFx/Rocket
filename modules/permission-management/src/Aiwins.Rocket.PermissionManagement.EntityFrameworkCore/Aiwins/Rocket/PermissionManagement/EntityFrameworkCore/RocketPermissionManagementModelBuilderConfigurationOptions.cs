using Aiwins.Rocket.EntityFrameworkCore.Modeling;
using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement.EntityFrameworkCore {
    public class RocketPermissionManagementModelBuilderConfigurationOptions : RocketModelBuilderConfigurationOptions {
        public RocketPermissionManagementModelBuilderConfigurationOptions (
            [NotNull] string tablePrefix, [CanBeNull] string schema) : base (
            tablePrefix,
            schema) {

        }
    }
}