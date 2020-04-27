using System;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace Aiwins.Rocket {
    /// <summary>
    /// 此异常信息可以直接展示给用户
    /// </summary>
    [Serializable]
    public class UserFriendlyException : BusinessException, IUserFriendlyException {
        public UserFriendlyException (
            string message,
            string code = null,
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning) : base (
            code,
            message,
            details,
            innerException,
            logLevel) {
            Details = details;
        }

        /// <summary>
        /// 包含序列化参数信息的构建函数
        /// </summary>
        public UserFriendlyException (SerializationInfo serializationInfo, StreamingContext context) : base (serializationInfo, context) {

        }
    }
}