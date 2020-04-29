using Aiwins.Rocket.MongoDB;
using JetBrains.Annotations;

namespace Aiwins.Rocket.SettingManagement.MongoDB {
    public class SettingManagementMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions {
        public SettingManagementMongoModelBuilderConfigurationOptions (
            [NotNull] string tablePrefix = "") : base (tablePrefix) { }
    }
}