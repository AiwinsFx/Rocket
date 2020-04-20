using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Aiwins.Rocket.RabbitMQ {
    public interface IRabbitMqMessageConsumer {
        Task BindAsync (string routingKey);

        Task UnbindAsync (string routingKey);

        void OnMessageReceived (Func<IModel, BasicDeliverEventArgs, Task> callback);
    }
}