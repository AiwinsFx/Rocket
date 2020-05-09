using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using Aiwins.Rocket.Logging;
using Microsoft.Extensions.Logging;

namespace Aiwins.Rocket.Validation {
    /// <summary>
    /// 验证异常类
    /// </summary>
    [Serializable]
    public class RocketValidationException : RocketException,
        IHasLogLevel,
        IHasValidationErrors,
        IExceptionWithSelfLogging {
            /// <summary>
            /// 验证的错误信息
            /// </summary>
            public IList<ValidationResult> ValidationErrors { get; }

            /// <summary>
            /// 日志级别
            /// 默认值: Warn.
            /// </summary>
            public LogLevel LogLevel { get; set; }

            /// <summary>
            /// 默认构造函数
            /// </summary>
            public RocketValidationException () {
                ValidationErrors = new List<ValidationResult> ();
                LogLevel = LogLevel.Warning;
            }

            /// <summary>
            /// 可序列化的构造函数
            /// </summary>
            public RocketValidationException (SerializationInfo serializationInfo, StreamingContext context) : base (serializationInfo, context) {
                ValidationErrors = new List<ValidationResult> ();
                LogLevel = LogLevel.Warning;
            }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="message">异常消息</param>
            public RocketValidationException (string message) : base (message) {
                ValidationErrors = new List<ValidationResult> ();
                LogLevel = LogLevel.Warning;
            }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="validationErrors">验证错误</param>
            public RocketValidationException (IList<ValidationResult> validationErrors) {
                ValidationErrors = validationErrors;
                LogLevel = LogLevel.Warning;
            }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="message">异常消息</param>
            /// <param name="validationErrors">验证错误</param>
            public RocketValidationException (string message, IList<ValidationResult> validationErrors) : base (message) {
                ValidationErrors = validationErrors;
                LogLevel = LogLevel.Warning;
            }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="message">异常消息</param>
            /// <param name="innerException">异常</param>
            public RocketValidationException (string message, Exception innerException) : base (message, innerException) {
                ValidationErrors = new List<ValidationResult> ();
                LogLevel = LogLevel.Warning;
            }

            public void Log (ILogger logger) {
                if (ValidationErrors.IsNullOrEmpty ()) {
                    return;
                }

                logger.LogWithLevel (LogLevel, "There are " + ValidationErrors.Count + " validation errors:");
                foreach (var validationResult in ValidationErrors) {
                    var memberNames = "";
                    if (validationResult.MemberNames != null && validationResult.MemberNames.Any ()) {
                        memberNames = " (" + string.Join (", ", validationResult.MemberNames) + ")";
                    }

                    logger.LogWithLevel (LogLevel, validationResult.ErrorMessage + memberNames);
                }
            }
        }
}