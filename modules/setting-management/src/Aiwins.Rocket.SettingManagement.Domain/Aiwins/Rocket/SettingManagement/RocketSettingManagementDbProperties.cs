using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.SettingManagement {
    public static class RocketSettingManagementDbProperties {
        public static string DbTablePrefix { get; set; } = RocketCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = RocketCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "RocketSettingManagement";
    }
}