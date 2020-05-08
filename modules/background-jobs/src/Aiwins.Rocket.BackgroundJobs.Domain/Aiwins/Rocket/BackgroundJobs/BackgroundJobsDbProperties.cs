using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.BackgroundJobs {
    public static class BackgroundJobsDbProperties {
        public static string DbTablePrefix { get; set; } = RocketCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = RocketCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "RocketBackgroundJobs";
    }
}