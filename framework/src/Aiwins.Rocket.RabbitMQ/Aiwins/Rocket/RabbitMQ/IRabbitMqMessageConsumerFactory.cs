namespace Aiwins.Rocket.RabbitMQ {
    public interface IRabbitMqMessageConsumerFactory {
        /// <summary>
        /// 创建一个新的消费实例 <see cref="IRabbitMqMessageConsumer"/>
        /// 避免创建太多的消费者，导致未释放内存溢出
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        IRabbitMqMessageConsumer Create (
            ExchangeDeclareConfiguration exchange,
            QueueDeclareConfiguration queue,
            string connectionName = null
        );
    }
}