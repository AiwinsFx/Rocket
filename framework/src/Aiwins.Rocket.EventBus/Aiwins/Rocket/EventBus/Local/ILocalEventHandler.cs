using System.Threading.Tasks;

namespace Aiwins.Rocket.EventBus.Local {
    public interface ILocalEventHandler<in TEvent> : IEventHandler {
        /// <summary>
        /// 实现此接口对事件进行处理
        /// </summary>
        /// <param name="eventData">事件相关的数据</param>
        Task HandleEventAsync (TEvent eventData);
    }
}