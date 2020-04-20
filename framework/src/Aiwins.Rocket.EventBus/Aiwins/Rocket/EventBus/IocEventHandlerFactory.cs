using System;
using System.Collections.Generic;
using System.Linq;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.EventBus {
    /// <summary>
    /// 通过IOC容器的方式实现事件处理函数工厂 <see cref="IEventHandlerFactory"/> 接口
    /// </summary>
    public class IocEventHandlerFactory : IEventHandlerFactory, IDisposable {
        public Type HandlerType { get; }

        protected IServiceScopeFactory ScopeFactory { get; }

        public IocEventHandlerFactory (IServiceScopeFactory scopeFactory, Type handlerType) {
            ScopeFactory = scopeFactory;
            HandlerType = handlerType;
        }

        /// <summary>
        /// 从IOC容器中解析事件处理函数对象
        /// </summary>
        /// <returns>解析的事件处理函数对象</returns>
        public IEventHandlerDisposeWrapper GetHandler () {
            var scope = ScopeFactory.CreateScope ();
            return new EventHandlerDisposeWrapper (
                (IEventHandler) scope.ServiceProvider.GetRequiredService (HandlerType),
                () => scope.Dispose ()
            );
        }

        public bool IsInFactories (List<IEventHandlerFactory> handlerFactories) {
            return handlerFactories
                .OfType<IocEventHandlerFactory> ()
                .Any (f => f.HandlerType == HandlerType);
        }

        public void Dispose () {

        }
    }
}