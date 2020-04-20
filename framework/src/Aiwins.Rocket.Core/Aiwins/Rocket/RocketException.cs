using System;
using System.Runtime.Serialization;

namespace Aiwins.Rocket {
    /// <summary>
    /// 应用程序异常
    /// </summary>
    public class RocketException : Exception {
        /// <summary>
        /// 创建RocketException <see cref="RocketException"/> 对象。
        /// </summary>
        public RocketException () {

        }

        /// <summary>
        /// 创建RocketException <see cref="RocketException"/> 对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        public RocketException (string message) : base (message) {

        }

        /// <summary>
        /// 创建RocketException <see cref="RocketException"/> 对象。
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">异常</param>
        public RocketException (string message, Exception innerException) : base (message, innerException) {

        }

        /// <summary>
        /// 包含序列化参数信息的构建函数
        /// </summary>
        public RocketException (SerializationInfo serializationInfo, StreamingContext context) : base (serializationInfo, context) {

        }
    }
}