using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Aiwins.Rocket.ExceptionHandling {
    public class ExceptionNotificationContext {
        /// <summary>
        /// 异常
        /// </summary>
        [NotNull]
        public Exception Exception { get; }

        public LogLevel LogLevel { get; }

        /// <summary>
        /// 异常是否被正常处理
        /// </summary>
        public bool Handled { get; }

        public ExceptionNotificationContext (
            [NotNull] Exception exception,
            LogLevel? logLevel = null,
            bool handled = true) {
            Exception = Check.NotNull (exception, nameof (exception));
            LogLevel = logLevel ?? exception.GetLogLevel ();
            Handled = handled;
        }
    }
}