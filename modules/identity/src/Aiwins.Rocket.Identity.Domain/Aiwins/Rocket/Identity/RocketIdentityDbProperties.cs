using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.Identity {
    public static class RocketIdentityDbProperties {
        public static string DbTablePrefix { get; set; } = RocketCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = RocketCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "RocketIdentity";
    }
}