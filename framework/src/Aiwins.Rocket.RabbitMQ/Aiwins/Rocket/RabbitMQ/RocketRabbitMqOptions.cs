namespace Aiwins.Rocket.RabbitMQ {
    public class RocketRabbitMqOptions {
        public RabbitMqConnections Connections { get; }

        public RocketRabbitMqOptions () {
            Connections = new RabbitMqConnections ();
        }
    }
}