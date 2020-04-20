using System;
using System.Runtime.Serialization;
using Aiwins.Rocket.ExceptionHandling;

namespace Aiwins.Rocket.Http.Client {
    [Serializable]
    public class RocketRemoteCallException : RocketException, IHasErrorCode, IHasErrorDetails {
        public string Code => Error?.Code;

        public string Details => Error?.Details;

        public RemoteServiceErrorInfo Error { get; set; }

        public RocketRemoteCallException () {

        }

        public RocketRemoteCallException (SerializationInfo serializationInfo, StreamingContext context) : base (serializationInfo, context) {

        }

        public RocketRemoteCallException (RemoteServiceErrorInfo error) : base (error.Message) {
            Error = error;
        }
    }
}