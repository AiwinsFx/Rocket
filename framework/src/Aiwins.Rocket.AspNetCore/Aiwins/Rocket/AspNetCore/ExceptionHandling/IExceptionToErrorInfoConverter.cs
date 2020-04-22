using System;
using Aiwins.Rocket.Http;

namespace Aiwins.Rocket.AspNetCore.ExceptionHandling {
    /// <summary>
    /// This interface can be implemented to convert an <see cref="Exception"/> object to an <see cref="RemoteServiceErrorInfo"/> object. 
    /// Implements Chain Of Responsibility pattern.
    /// </summary>
    public interface IExceptionToErrorInfoConverter {
        /// <summary>
        /// Converter method.
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns>Error info or null</returns>
        RemoteServiceErrorInfo Convert (Exception exception);
    }
}