using System.Collections.Generic;
using System.Runtime.Serialization;
using NUglify;

namespace Aiwins.Rocket.Minify.NUglify {
    public class NUglifyException : RocketException {
        public List<UglifyError> Errors { get; set; }

        public NUglifyException (string message, List<UglifyError> errors) : base (message) {
            Errors = errors;
        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public NUglifyException (SerializationInfo serializationInfo, StreamingContext context) : base (serializationInfo, context) {

        }
    }
}