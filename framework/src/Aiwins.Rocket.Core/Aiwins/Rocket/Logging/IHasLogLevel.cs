using Microsoft.Extensions.Logging;

namespace Aiwins.Rocket.Logging {
    public interface IHasLogLevel {
        /// <summary>
        /// 日志级别
        /// </summary>
        LogLevel LogLevel { get; set; }
    }
}