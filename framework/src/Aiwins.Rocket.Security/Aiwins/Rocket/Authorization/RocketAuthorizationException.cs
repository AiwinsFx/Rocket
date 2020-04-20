using System;
using System.Runtime.Serialization;
using Aiwins.Rocket.Logging;
using Microsoft.Extensions.Logging;

namespace Aiwins.Rocket.Authorization {
    /// <summary>
    /// 认证异常信息
    /// </summary>
    [Serializable]
    public class RocketAuthorizationException : RocketException, IHasLogLevel {
        /// <summary>
        /// 日志级别
        /// 默认值: Warn.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// 创建一个新的认证异常 <see cref="RocketAuthorizationException"/> 对象。
        /// </summary>
        public RocketAuthorizationException () {
            LogLevel = LogLevel.Warning;
        }

        /// <summary>
        /// 创建一个新的认证异常 <see cref="RocketAuthorizationException"/> 对象。
        /// </summary>
        public RocketAuthorizationException (SerializationInfo serializationInfo, StreamingContext context) : base (serializationInfo, context) {

        }

        /// <summary>
        /// 创建一个新的认证异常 <see cref="RocketAuthorizationException"/> 对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        public RocketAuthorizationException (string message) : base (message) {
            LogLevel = LogLevel.Warning;
        }

        /// <summary>
        /// 创建一个新的认证异常 <see cref="RocketAuthorizationException"/> 对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">异常</param>
        public RocketAuthorizationException (string message, Exception innerException) : base (message, innerException) {
            LogLevel = LogLevel.Warning;
        }
    }
}