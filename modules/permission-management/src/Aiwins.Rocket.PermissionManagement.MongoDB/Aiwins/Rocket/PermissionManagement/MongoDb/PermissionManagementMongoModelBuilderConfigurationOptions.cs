using Aiwins.Rocket.MongoDB;
using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement.MongoDB {
    public class PermissionManagementMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions {
        public PermissionManagementMongoModelBuilderConfigurationOptions (
            [NotNull] string tablePrefix = "") : base (tablePrefix) { }
    }
}