using System.Collections.Generic;
using System.Net;

namespace Aiwins.Rocket.AspNetCore.ExceptionHandling {
    public class RocketExceptionHttpStatusCodeOptions {
        public IDictionary<string, HttpStatusCode> ErrorCodeToHttpStatusCodeMappings { get; }

        public RocketExceptionHttpStatusCodeOptions () {
            ErrorCodeToHttpStatusCodeMappings = new Dictionary<string, HttpStatusCode> ();
        }

        public void Map (string errorCode, HttpStatusCode httpStatusCode) {
            ErrorCodeToHttpStatusCodeMappings[errorCode] = httpStatusCode;
        }
    }
}