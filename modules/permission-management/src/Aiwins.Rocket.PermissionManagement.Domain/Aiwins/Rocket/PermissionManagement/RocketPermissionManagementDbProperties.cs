using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.PermissionManagement
{
    public static class RocketPermissionManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = RocketCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = RocketCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "RocketPermissionManagement";
    }
}
