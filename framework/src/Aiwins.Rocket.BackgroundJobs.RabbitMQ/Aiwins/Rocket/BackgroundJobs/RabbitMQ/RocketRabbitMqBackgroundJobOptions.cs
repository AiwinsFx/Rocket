using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.BackgroundJobs.RabbitMQ {
    public class RocketRabbitMqBackgroundJobOptions {
        /// <summary>
        /// 键: 工作参数类型
        /// </summary>
        public Dictionary<Type, JobQueueConfiguration> JobQueues { get; }

        /// <summary>
        /// 默认值: "RocketBackgroundJobs.".
        /// </summary>
        public string DefaultQueueNamePrefix { get; set; }

        public RocketRabbitMqBackgroundJobOptions () {
            JobQueues = new Dictionary<Type, JobQueueConfiguration> ();
            DefaultQueueNamePrefix = "RocketBackgroundJobs.";
        }
    }
}