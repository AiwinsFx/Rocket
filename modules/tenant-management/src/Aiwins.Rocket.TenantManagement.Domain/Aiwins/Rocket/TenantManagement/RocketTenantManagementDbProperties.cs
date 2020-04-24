using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.TenantManagement
{
    public static class RocketTenantManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = RocketCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = RocketCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "RocketTenantManagement";
    }
}
