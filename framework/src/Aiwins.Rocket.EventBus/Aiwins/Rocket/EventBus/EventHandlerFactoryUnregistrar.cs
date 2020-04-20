using System;

namespace Aiwins.Rocket.EventBus {
    /// <summary>
    /// 取消事件订阅工厂 <see cref="IEventHandlerFactory"/> ，在对象释放 <see cref="Dispose"/> 的时候取消订阅
    /// </summary>
    public class EventHandlerFactoryUnregistrar : IDisposable {
        private readonly IEventBus _eventBus;
        private readonly Type _eventType;
        private readonly IEventHandlerFactory _factory;

        public EventHandlerFactoryUnregistrar (IEventBus eventBus, Type eventType, IEventHandlerFactory factory) {
            _eventBus = eventBus;
            _eventType = eventType;
            _factory = factory;
        }

        public void Dispose () {
            _eventBus.Unsubscribe (_eventType, _factory);
        }
    }
}