namespace Aiwins.Rocket.Redis {
    public class RocketRedisOptions {
        public RedisConnections Connections { get; }

        public RocketRedisOptions () {
            Connections = new RedisConnections ();
        }
    }
}