using Aiwins.Rocket.MongoDB;
using JetBrains.Annotations;

namespace Aiwins.Rocket.TenantManagement.MongoDB {
    public class TenantManagementMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions {
        public TenantManagementMongoModelBuilderConfigurationOptions (
            [NotNull] string tablePrefix = "") : base (tablePrefix) { }
    }
}