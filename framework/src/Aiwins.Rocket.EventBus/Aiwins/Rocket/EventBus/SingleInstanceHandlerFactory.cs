using System.Collections.Generic;
using System.Linq;

namespace Aiwins.Rocket.EventBus {
    /// <summary>
    /// 单例方式实现了事件处理函数工厂 <see cref="IEventHandlerFactory"/> 接口
    /// </summary>
    /// <remarks>
    /// 此类总是返回同一个事件处理函数实例对象
    /// </remarks>
    public class SingleInstanceHandlerFactory : IEventHandlerFactory {
        /// <summary>
        /// 事件处理函数实例对象
        /// </summary>
        public IEventHandler HandlerInstance { get; }

        /// <summary>
        /// 单例
        /// </summary>
        /// <param name="handler"></param>
        public SingleInstanceHandlerFactory (IEventHandler handler) {
            HandlerInstance = handler;
        }

        public IEventHandlerDisposeWrapper GetHandler () {
            return new EventHandlerDisposeWrapper (HandlerInstance);
        }

        public bool IsInFactories (List<IEventHandlerFactory> handlerFactories) {
            return handlerFactories
                .OfType<SingleInstanceHandlerFactory> ()
                .Any (f => f.HandlerInstance == HandlerInstance);
        }
    }
}