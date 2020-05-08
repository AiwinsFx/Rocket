namespace Aiwins.Rocket.IdentityServer {
    public static class RocketIdentityServerDbProperties {
        public static string DbTablePrefix { get; set; } = "IdentityServer";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "RocketIdentityServer";
    }
}