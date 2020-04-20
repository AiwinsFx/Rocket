using System;
using System.Runtime.Serialization;
using Aiwins.Rocket.ExceptionHandling;
using Aiwins.Rocket.Logging;
using Microsoft.Extensions.Logging;

namespace Aiwins.Rocket {
    [Serializable]
    public class BusinessException : Exception,
        IBusinessException,
        IHasErrorCode,
        IHasErrorDetails,
        IHasLogLevel {
            public string Code { get; set; }

            public string Details { get; set; }

            public LogLevel LogLevel { get; set; }

            public BusinessException (
                string code = null,
                string message = null,
                string details = null,
                Exception innerException = null,
                LogLevel logLevel = LogLevel.Warning) : base (message, innerException) {
                Code = code;
                Details = details;
                LogLevel = logLevel;
            }

            public BusinessException (SerializationInfo serializationInfo, StreamingContext context) : base (serializationInfo, context) {

            }

            public BusinessException WithData (string name, object value) {
                Data[name] = value;
                return this;
            }
        }
}