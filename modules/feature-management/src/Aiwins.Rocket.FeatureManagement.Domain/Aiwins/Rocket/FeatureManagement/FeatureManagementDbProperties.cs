using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.FeatureManagement
{
    public static class FeatureManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = RocketCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = RocketCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "RocketFeatureManagement";
    }
}
