namespace System {
    /// <summary>
    /// EventHandler <see cref="EventHandler"/> 相关扩展方法。
    /// </summary>
    public static class RocketEventHandlerExtensions {
        /// <summary>
        /// 通过给定参数安全地调用事件处理函数。
        /// </summary>
        /// <param name="eventHandler">事件处理函数</param>
        /// <param name="sender">事件源</param>
        public static void InvokeSafely (this EventHandler eventHandler, object sender) {
            eventHandler.InvokeSafely (sender, EventArgs.Empty);
        }

        /// <summary>
        /// 通过给定参数安全地调用事件处理函数。
        /// </summary>
        /// <param name="eventHandler">事件处理函数</param>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件信息</param>
        public static void InvokeSafely (this EventHandler eventHandler, object sender, EventArgs e) {
            eventHandler?.Invoke (sender, e);
        }

        /// <summary>
        /// 通过给定参数安全地调用事件处理函数。
        /// </summary>
        /// <typeparam name="TEventArgs">事件信息类型<see cref="EventArgs"/></typeparam>
        /// <param name="eventHandler">事件处理函数</param>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件信息</param>
        public static void InvokeSafely<TEventArgs> (this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e)
        where TEventArgs : EventArgs {
            eventHandler?.Invoke (sender, e);
        }
    }
}