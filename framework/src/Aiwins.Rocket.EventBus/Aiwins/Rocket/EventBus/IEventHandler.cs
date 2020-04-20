using Aiwins.Rocket.EventBus.Distributed;

namespace Aiwins.Rocket.EventBus {
    /// <summary>
    /// 事件处理接口
    /// 可以实现 <see cref="ILocalEventHandler{TEvent}"/> 或者 <see cref="IDistributedEventHandler{TEvent}"/> 替换这个接口。
    /// </summary>
    public interface IEventHandler {

    }
}