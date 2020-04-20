using System;

namespace Aiwins.Rocket.EventBus.Distributed {
    /// <summary>
    /// 定义分布式事件总线接口
    /// </summary>
    public interface IDistributedEventBus : IEventBus {
        /// <summary>
        /// 事件订阅
        /// 事件发生时捕获事件并进行处理
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">事件处理程序</param>
        IDisposable Subscribe<TEvent> (IDistributedEventHandler<TEvent> handler)
        where TEvent : class;
    }
}