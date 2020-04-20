using System;
using RabbitMQ.Client;

namespace Aiwins.Rocket.RabbitMQ {
    public interface IConnectionPool : IDisposable {
        IConnection Get (string connectionName = null);
    }
}