using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.AuditLogging {
    public static class RocketAuditLoggingDbProperties {
        public static string DbTablePrefix { get; set; } = RocketCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = RocketCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "RocketAuditLogging";
    }
}