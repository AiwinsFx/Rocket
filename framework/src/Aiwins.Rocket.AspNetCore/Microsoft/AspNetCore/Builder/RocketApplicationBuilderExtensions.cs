using System;
using Aiwins.Rocket;
using Aiwins.Rocket.AspNetCore.Auditing;
using Aiwins.Rocket.AspNetCore.ExceptionHandling;
using Aiwins.Rocket.AspNetCore.Tracing;
using Aiwins.Rocket.AspNetCore.Uow;
using Aiwins.Rocket.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore.Builder {
    public static class RocketApplicationBuilderExtensions {
        private const string ExceptionHandlingMiddlewareMarker = "_RocketExceptionHandlingMiddleware_Added";

        public static void InitializeApplication ([NotNull] this IApplicationBuilder app) {
            Check.NotNull (app, nameof (app));

            app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>> ().Value = app;
            var application = app.ApplicationServices.GetRequiredService<IRocketApplicationWithExternalServiceProvider> ();
            var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime> ();

            applicationLifetime.ApplicationStopping.Register (() => {
                application.Shutdown ();
            });

            applicationLifetime.ApplicationStopped.Register (() => {
                application.Dispose ();
            });

            application.Initialize (app.ApplicationServices);
        }

        public static IApplicationBuilder UseAuditing (this IApplicationBuilder app) {
            return app
                .UseMiddleware<RocketAuditingMiddleware> ();
        }

        public static IApplicationBuilder UseUnitOfWork (this IApplicationBuilder app) {
            return app
                .UseRocketExceptionHandling ()
                .UseMiddleware<RocketUnitOfWorkMiddleware> ();
        }

        public static IApplicationBuilder UseCorrelationId (this IApplicationBuilder app) {
            return app
                .UseMiddleware<RocketCorrelationIdMiddleware> ();
        }

        public static IApplicationBuilder UseRocketRequestLocalization (this IApplicationBuilder app,
            Action<RequestLocalizationOptions> optionsAction = null) {
            app.ApplicationServices
                .GetRequiredService<IRocketRequestLocalizationOptionsProvider> ()
                .InitLocalizationOptions (optionsAction);

            return app.UseMiddleware<RocketRequestLocalizationMiddleware> ();
        }

        public static IApplicationBuilder UseRocketExceptionHandling (this IApplicationBuilder app) {
            if (app.Properties.ContainsKey (ExceptionHandlingMiddlewareMarker)) {
                return app;
            }

            app.Properties[ExceptionHandlingMiddlewareMarker] = true;
            return app.UseMiddleware<RocketExceptionHandlingMiddleware> ();
        }
    }
}