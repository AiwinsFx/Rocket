using System;
using RabbitMQ.Client;

namespace Aiwins.Rocket.RabbitMQ {
    public interface IChannelAccessor : IDisposable {
        /// <summary>
        /// 通道
        /// 建议不要释放通道 <see cref="Channel"/> 对象
        /// 作为替代，可以在使用后释放通道访问器 <see cref="IChannelAccessor"/>
        /// </summary>
        IModel Channel { get; }

        /// <summary>
        /// 通道的名称
        /// </summary>
        string Name { get; }
    }
}