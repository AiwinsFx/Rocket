using JetBrains.Annotations;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.PermissionManagement.MongoDB
{
    public class PermissionManagementMongoModelBuilderConfigurationOptions : RocketMongoModelBuilderConfigurationOptions
    {
        public PermissionManagementMongoModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "")
            : base(tablePrefix)
        {
        }
    }
}