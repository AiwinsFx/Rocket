using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiwins.Rocket.EventBus {
    /// <summary>
    /// 瞬态方式实现了事件处理函数工厂 <see cref="IEventHandlerFactory"/> 接口
    /// </summary>
    /// <remarks>
    /// 此类总是返回立即创建的处理函数实例对象
    /// </remarks>
    public class TransientEventHandlerFactory<THandler> : TransientEventHandlerFactory, IEventHandlerFactory
    where THandler : IEventHandler, new () {
        public TransientEventHandlerFactory () : base (typeof (THandler)) {

        }

        protected override IEventHandler CreateHandler () {
            return new THandler ();
        }
    }

    /// <summary>
    /// 瞬态方式实现了事件处理函数工厂 <see cref="IEventHandlerFactory"/> 接口
    /// </summary>
    /// <remarks>
    /// 此类总是返回立即创建的处理函数实例对象
    /// </remarks>
    public class TransientEventHandlerFactory : IEventHandlerFactory {
        public Type HandlerType { get; }

        public TransientEventHandlerFactory (Type handlerType) {
            HandlerType = handlerType;
        }

        /// <summary>
        /// 创建一个新的事件处理函数对象实例
        /// </summary>
        /// <returns>事件处理函数对象实例</returns>
        public virtual IEventHandlerDisposeWrapper GetHandler () {
            var handler = CreateHandler ();
            return new EventHandlerDisposeWrapper (
                handler,
                () => (handler as IDisposable)?.Dispose ()
            );
        }

        public bool IsInFactories (List<IEventHandlerFactory> handlerFactories) {
            return handlerFactories
                .OfType<TransientEventHandlerFactory> ()
                .Any (f => f.HandlerType == HandlerType);
        }

        protected virtual IEventHandler CreateHandler () {
            return (IEventHandler) Activator.CreateInstance (HandlerType);
        }
    }
}