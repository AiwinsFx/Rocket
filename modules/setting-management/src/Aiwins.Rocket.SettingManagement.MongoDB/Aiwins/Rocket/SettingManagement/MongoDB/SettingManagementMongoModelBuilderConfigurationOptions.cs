using JetBrains.Annotations;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.SettingManagement.MongoDB
{
    public class SettingManagementMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions
    {
        public SettingManagementMongoModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "")
            : base(tablePrefix)
        {
        }
    }
}