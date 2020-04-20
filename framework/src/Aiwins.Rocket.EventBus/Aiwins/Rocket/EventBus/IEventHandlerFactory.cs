using System.Collections.Generic;

namespace Aiwins.Rocket.EventBus {
    /// <summary>
    /// 用于创建和提供事件处理函数的工厂类
    /// </summary>
    public interface IEventHandlerFactory {
        /// <summary>
        /// 获取事件处理函数
        /// </summary>
        /// <returns>事件处理函数</returns>
        IEventHandlerDisposeWrapper GetHandler ();

        bool IsInFactories (List<IEventHandlerFactory> handlerFactories);
    }
}