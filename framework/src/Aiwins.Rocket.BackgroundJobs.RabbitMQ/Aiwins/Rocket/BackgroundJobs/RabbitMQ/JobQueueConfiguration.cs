using System;
using Aiwins.Rocket.RabbitMQ;

namespace Aiwins.Rocket.BackgroundJobs.RabbitMQ {
    public class JobQueueConfiguration : QueueDeclareConfiguration {
        public Type JobArgsType { get; }

        public string ConnectionName { get; set; }

        public JobQueueConfiguration (
            Type jobArgsType,
            string queueName,
            string connectionName = null,
            bool durable = true,
            bool exclusive = false,
            bool autoDelete = false) : base (
            queueName,
            durable,
            exclusive,
            autoDelete) {
            JobArgsType = jobArgsType;
            ConnectionName = connectionName;
        }
    }
}