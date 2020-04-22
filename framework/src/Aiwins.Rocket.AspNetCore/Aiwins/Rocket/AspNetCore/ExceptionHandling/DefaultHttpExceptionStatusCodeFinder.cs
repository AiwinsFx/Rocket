using System;
using System.Net;
using Aiwins.Rocket.Authorization;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.ExceptionHandling;
using Aiwins.Rocket.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.ExceptionHandling {
    public class DefaultHttpExceptionStatusCodeFinder : IHttpExceptionStatusCodeFinder, ITransientDependency {
        protected RocketExceptionHttpStatusCodeOptions Options { get; }

        public DefaultHttpExceptionStatusCodeFinder (
            IOptions<RocketExceptionHttpStatusCodeOptions> options) {
            Options = options.Value;
        }

        public virtual HttpStatusCode GetStatusCode (HttpContext httpContext, Exception exception) {
            if (exception is IHasErrorCode exceptionWithErrorCode &&
                !exceptionWithErrorCode.Code.IsNullOrWhiteSpace ()) {
                if (Options.ErrorCodeToHttpStatusCodeMappings.TryGetValue (exceptionWithErrorCode.Code, out var status)) {
                    return status;
                }
            }

            if (exception is RocketAuthorizationException) {
                return httpContext.User.Identity.IsAuthenticated ?
                    HttpStatusCode.Forbidden :
                    HttpStatusCode.Unauthorized;
            }

            //TODO: Handle SecurityException..?

            if (exception is RocketValidationException) {
                return HttpStatusCode.BadRequest;
            }

            if (exception is EntityNotFoundException) {
                return HttpStatusCode.NotFound;
            }

            if (exception is NotImplementedException) {
                return HttpStatusCode.NotImplemented;
            }

            if (exception is IBusinessException) {
                return HttpStatusCode.Forbidden;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}