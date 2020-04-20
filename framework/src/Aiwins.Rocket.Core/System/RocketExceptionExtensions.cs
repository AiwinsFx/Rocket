using System.Runtime.ExceptionServices;
using Aiwins.Rocket.Logging;
using Microsoft.Extensions.Logging;

namespace System {
    /// <summary>
    /// Exception <see cref="Exception"/> 相关的扩展方法。
    /// </summary>
    public static class RocketExceptionExtensions {
        /// <summary>
        /// 通过ExceptionDispatchInfo.Capture <see cref="ExceptionDispatchInfo.Capture"/> 方法重新抛出异常
        /// 同时保留堆栈跟踪.
        /// </summary>
        /// <param name="exception">异常</param>
        public static void ReThrow (this Exception exception) {
            ExceptionDispatchInfo.Capture (exception).Throw ();
        }

        /// <summary>
        /// 尝试获取实现了 <see cref="IHasLogLevel"/> 接口的异常 <paramref name="exception"/> 日志级别
        /// 如果不存在则返回默认 <paramref name="defaultLevel"/> 日志级别
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="defaultLevel"></param>
        /// <returns></returns>
        public static LogLevel GetLogLevel (this Exception exception, LogLevel defaultLevel = LogLevel.Error) {
            return (exception as IHasLogLevel)?.LogLevel ?? defaultLevel;
        }
    }
}