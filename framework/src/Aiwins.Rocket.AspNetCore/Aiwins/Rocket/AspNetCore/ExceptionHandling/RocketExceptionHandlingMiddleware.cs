using System;
using System.Threading.Tasks;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Uow;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.ExceptionHandling;
using Aiwins.Rocket.Http;
using Aiwins.Rocket.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Aiwins.Rocket.AspNetCore.ExceptionHandling {
    public class RocketExceptionHandlingMiddleware : IMiddleware, ITransientDependency {
        private readonly ILogger<RocketUnitOfWorkMiddleware> _logger;

        private readonly Func<object, Task> _clearCacheHeadersDelegate;

        public RocketExceptionHandlingMiddleware (ILogger<RocketUnitOfWorkMiddleware> logger) {
            _logger = logger;

            _clearCacheHeadersDelegate = ClearCacheHeaders;
        }

        public async Task InvokeAsync (HttpContext context, RequestDelegate next) {
            try {
                await next (context);
            } catch (Exception ex) {
                // We can't do anything if the response has already started, just abort.
                if (context.Response.HasStarted) {
                    _logger.LogWarning ("An exception occurred, but response has already started!");
                    throw;
                }

                if (context.Items["_RocketActionInfo"] is RocketActionInfoInHttpContext actionInfo) {
                    if (actionInfo.IsObjectResult) //TODO: Align with RocketExceptionFilter.ShouldHandleException!
                    {
                        await HandleAndWrapException (context, ex);
                        return;
                    }
                }

                throw;
            }
        }

        private async Task HandleAndWrapException (HttpContext httpContext, Exception exception) {
            _logger.LogException (exception);

            var errorInfoConverter = httpContext.RequestServices.GetRequiredService<IExceptionToErrorInfoConverter> ();
            var statusCodeFinder = httpContext.RequestServices.GetRequiredService<IHttpExceptionStatusCodeFinder> ();
            var jsonSerializer = httpContext.RequestServices.GetRequiredService<IJsonSerializer> ();

            httpContext.Response.Clear ();
            httpContext.Response.StatusCode = (int) statusCodeFinder.GetStatusCode (httpContext, exception);
            httpContext.Response.OnStarting (_clearCacheHeadersDelegate, httpContext.Response);
            httpContext.Response.Headers.Add (RocketHttpConsts.RocketErrorFormat, "true");

            await httpContext.Response.WriteAsync (
                jsonSerializer.Serialize (
                    new RemoteServiceErrorResponse (
                        errorInfoConverter.Convert (exception)
                    )
                )
            );

            await httpContext
                .RequestServices
                .GetRequiredService<IExceptionNotifier> ()
                .NotifyAsync (
                    new ExceptionNotificationContext (exception)
                );
        }

        private Task ClearCacheHeaders (object state) {
            var response = (HttpResponse) state;

            response.Headers[HeaderNames.CacheControl] = "no-cache";
            response.Headers[HeaderNames.Pragma] = "no-cache";
            response.Headers[HeaderNames.Expires] = "-1";
            response.Headers.Remove (HeaderNames.ETag);

            return Task.CompletedTask;
        }
    }
}