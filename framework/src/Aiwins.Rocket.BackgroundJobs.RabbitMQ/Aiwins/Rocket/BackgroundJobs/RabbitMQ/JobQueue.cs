using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.ExceptionHandling;
using Aiwins.Rocket.RabbitMQ;
using Aiwins.Rocket.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Nito.AsyncEx;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Aiwins.Rocket.BackgroundJobs.RabbitMQ {
    public class JobQueue<TArgs> : IJobQueue<TArgs> {
        private const string ChannelPrefix = "JobQueue.";

        protected BackgroundJobConfiguration JobConfiguration { get; }
        protected JobQueueConfiguration QueueConfiguration { get; }
        protected IChannelAccessor ChannelAccessor { get; private set; }
        protected EventingBasicConsumer Consumer { get; private set; }

        public ILogger<JobQueue<TArgs>> Logger { get; set; }

        protected RocketBackgroundJobOptions RocketBackgroundJobOptions { get; }
        protected RocketRabbitMqBackgroundJobOptions RocketRabbitMqBackgroundJobOptions { get; }
        protected IChannelPool ChannelPool { get; }
        protected IRabbitMqSerializer Serializer { get; }
        protected IBackgroundJobExecuter JobExecuter { get; }
        protected IServiceScopeFactory ServiceScopeFactory { get; }
        protected IExceptionNotifier ExceptionNotifier { get; }

        protected SemaphoreSlim SyncObj = new SemaphoreSlim (1, 1);
        protected bool IsDisposed { get; private set; }

        public JobQueue (
            IOptions<RocketBackgroundJobOptions> backgroundJobOptions,
            IOptions<RocketRabbitMqBackgroundJobOptions> rabbitMqRocketBackgroundJobOptions,
            IChannelPool channelPool,
            IRabbitMqSerializer serializer,
            IBackgroundJobExecuter jobExecuter,
            IServiceScopeFactory serviceScopeFactory,
            IExceptionNotifier exceptionNotifier) {
            RocketBackgroundJobOptions = backgroundJobOptions.Value;
            RocketRabbitMqBackgroundJobOptions = rabbitMqRocketBackgroundJobOptions.Value;
            Serializer = serializer;
            JobExecuter = jobExecuter;
            ServiceScopeFactory = serviceScopeFactory;
            ExceptionNotifier = exceptionNotifier;
            ChannelPool = channelPool;

            JobConfiguration = RocketBackgroundJobOptions.GetJob (typeof (TArgs));
            QueueConfiguration = GetOrCreateJobQueueConfiguration ();

            Logger = NullLogger<JobQueue<TArgs>>.Instance;
        }

        protected virtual JobQueueConfiguration GetOrCreateJobQueueConfiguration () {
            return RocketRabbitMqBackgroundJobOptions.JobQueues.GetOrDefault (typeof (TArgs)) ??
                new JobQueueConfiguration (
                    typeof (TArgs),
                    RocketRabbitMqBackgroundJobOptions.DefaultQueueNamePrefix + JobConfiguration.JobName
                );
        }

        public virtual async Task<string> EnqueueAsync (
            TArgs args,
            BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null) {
            CheckDisposed ();

            using (await SyncObj.LockAsync ()) {
                await EnsureInitializedAsync ();

                await PublishAsync (args, priority, delay);

                return null;
            }
        }

        public virtual async Task StartAsync (CancellationToken cancellationToken = default) {
            CheckDisposed ();

            if (!RocketBackgroundJobOptions.IsJobExecutionEnabled) {
                return;
            }

            using (await SyncObj.LockAsync ()) {
                await EnsureInitializedAsync ();
            }
        }

        public virtual Task StopAsync (CancellationToken cancellationToken = default) {
            Dispose ();
            return Task.CompletedTask;
        }

        public virtual void Dispose () {
            if (IsDisposed) {
                return;
            }

            IsDisposed = true;

            ChannelAccessor?.Dispose ();
        }

        protected virtual Task EnsureInitializedAsync () {
            if (ChannelAccessor != null) {
                return Task.CompletedTask;
            }

            ChannelAccessor = ChannelPool.Acquire (
                ChannelPrefix + QueueConfiguration.QueueName,
                QueueConfiguration.ConnectionName
            );

            var result = QueueConfiguration.Declare (ChannelAccessor.Channel);
            Logger.LogDebug ($"RabbitMQ Queue '{QueueConfiguration.QueueName}' has {result.MessageCount} messages and {result.ConsumerCount} consumers.");

            if (RocketBackgroundJobOptions.IsJobExecutionEnabled) {
                Consumer = new EventingBasicConsumer (ChannelAccessor.Channel);
                Consumer.Received += MessageReceived;

                //TODO: What BasicConsume returns?
                ChannelAccessor.Channel.BasicConsume (
                    queue: QueueConfiguration.QueueName,
                    autoAck: false,
                    consumer: Consumer
                );
            }

            return Task.CompletedTask;
        }

        protected virtual Task PublishAsync (
            TArgs args,
            BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null) {
            //TODO: How to handle priority & delay?

            ChannelAccessor.Channel.BasicPublish (
                exchange: "",
                routingKey : QueueConfiguration.QueueName,
                basicProperties : CreateBasicPropertiesToPublish (),
                body : Serializer.Serialize (args)
            );

            return Task.CompletedTask;
        }

        protected virtual IBasicProperties CreateBasicPropertiesToPublish () {
            var properties = ChannelAccessor.Channel.CreateBasicProperties ();
            properties.Persistent = true;
            return properties;
        }

        protected virtual void MessageReceived (object sender, BasicDeliverEventArgs ea) {
            using (var scope = ServiceScopeFactory.CreateScope ()) {
                var context = new JobExecutionContext (
                    scope.ServiceProvider,
                    JobConfiguration.JobType,
                    Serializer.Deserialize (ea.Body, typeof (TArgs))
                );

                try {
                    AsyncHelper.RunSync (() => JobExecuter.ExecuteAsync (context));
                    ChannelAccessor.Channel.BasicAck (deliveryTag: ea.DeliveryTag, multiple: false);
                } catch (BackgroundJobExecutionException) {
                    //TODO: Reject like that?
                    ChannelAccessor.Channel.BasicReject (deliveryTag: ea.DeliveryTag, requeue: true);
                } catch (Exception) {
                    //TODO: Reject like that?
                    ChannelAccessor.Channel.BasicReject (deliveryTag: ea.DeliveryTag, requeue: false);
                }
            }
        }

        protected void CheckDisposed () {
            if (IsDisposed) {
                throw new RocketException ("This object is disposed!");
            }
        }
    }
}