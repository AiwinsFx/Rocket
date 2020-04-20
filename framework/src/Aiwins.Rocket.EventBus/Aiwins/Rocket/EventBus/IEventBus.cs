using System;
using System.Threading.Tasks;
using Aiwins.Rocket.EventBus.Local;

namespace Aiwins.Rocket.EventBus {
    public interface IEventBus {
        /// <summary>
        /// 触发指定事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="eventData">事件相关的数据</param>
        /// <returns>事件任务</returns>
        Task PublishAsync<TEvent> (TEvent eventData)
        where TEvent : class;

        /// <summary>
        /// 触发指定事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventData">事件相关的数据</param>
        /// <returns>事件任务</returns>
        Task PublishAsync (Type eventType, object eventData);

        /// <summary>
        /// 订阅指定事件
        /// 在事件触发时，对事件进行捕获并处理
        /// </summary>
        /// <param name="action">事件处理函数</param>
        /// <typeparam name="TEvent">事件类型</typeparam>
        IDisposable Subscribe<TEvent> (Func<TEvent, Task> action)
        where TEvent : class;

        /// <summary>
        /// 订阅指定事件
        /// 通过事件处理 <see cref="THandler"/> 对象对订阅的事件进行捕获处理
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <typeparam name="THandler">事件处理对象的类型</typeparam>
        IDisposable Subscribe<TEvent, THandler> ()
        where TEvent : class
        where THandler : IEventHandler, new ();

        /// <summary>
        /// 订阅指定事件
        /// 通过事件处理对象对订阅的事件进行捕获处理
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">事件处理对象</param>
        IDisposable Subscribe (Type eventType, IEventHandler handler);

        /// <summary>
        /// 订阅指定事件
        /// 通过函数工厂对订阅的事件进行捕获处理
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="factory">函数工厂</param>
        IDisposable Subscribe<TEvent> (IEventHandlerFactory factory)
        where TEvent : class;

        /// <summary>
        /// 订阅指定事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="factory">函数工厂</param>
        IDisposable Subscribe (Type eventType, IEventHandlerFactory factory);

        /// <summary>
        /// 取消指定事件的订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="action"></param>
        void Unsubscribe<TEvent> (Func<TEvent, Task> action)
        where TEvent : class;

        /// <summary>
        /// 取消指定事件的订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">之前订阅的事件处理函数</param>
        void Unsubscribe<TEvent> (ILocalEventHandler<TEvent> handler)
        where TEvent : class;

        /// <summary>
        /// 取消指定事件的订阅
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">之前订阅的事件处理函数</param>
        void Unsubscribe (Type eventType, IEventHandler handler);

        /// <summary>
        /// 取消指定事件的订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="factory">之前订阅的事件处理函数工厂</param>
        void Unsubscribe<TEvent> (IEventHandlerFactory factory)
        where TEvent : class;

        /// <summary>
        /// 取消指定事件的订阅
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="factory">之前订阅的事件处理函数工厂</param>
        void Unsubscribe (Type eventType, IEventHandlerFactory factory);

        /// <summary>
        /// 取消指定事件的所有订阅
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void UnsubscribeAll<TEvent> ()
        where TEvent : class;

        /// <summary>
        /// 取消指定事件的所有订阅
        /// </summary>
        /// <param name="eventType">事件类型</param>
        void UnsubscribeAll (Type eventType);
    }
}