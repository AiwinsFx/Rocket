using System;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.EventBus {
    /// <summary>
    /// 实现了 <see cref="ILocalEventHandler{TEvent}"/> 接口的事件处理类
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    public class ActionEventHandler<TEvent>:
        ILocalEventHandler<TEvent>,
        ITransientDependency {
            /// <summary>
            /// 事件处理委托函数
            /// </summary>
            public Func<TEvent, Task> Action { get; }

            /// <summary>
            /// 创建一个新的 <see cref="ActionEventHandler{TEvent}"/> 对象
            /// </summary>
            /// <param name="handler">Action to handle the event</param>
            public ActionEventHandler (Func<TEvent, Task> handler) {
                Action = handler;
            }

            /// <summary>
            /// 执行事件处理
            /// </summary>
            /// <param name="eventData">事件相关的数据</param>
            public async Task HandleEventAsync (TEvent eventData) {
                await Action (eventData);
            }
        }
}