using System;
using System.Linq;
using Aiwins.Rocket.ApiVersioning;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.Conventions;
using Aiwins.Rocket.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Microsoft.Extensions.DependencyInjection {
    public static class RocketApiVersioningExtensions {
        public static IServiceCollection AddRocketApiVersioning (this IServiceCollection services, Action<ApiVersioningOptions> setupAction) {
            services.AddTransient<IRequestedApiVersion, HttpContextRequestedApiVersion> ();
            services.AddTransient<IApiControllerSpecification, RocketConventionalApiControllerSpecification> ();

            services.AddApiVersioning (setupAction);

            return services;
        }

        public static void ConfigureRocket (this ApiVersioningOptions options, RocketAspNetCoreMvcOptions mvcOptions) {
            foreach (var setting in mvcOptions.ConventionalControllers.ConventionalControllerSettings) {
                if (setting.ApiVersionConfigurer == null) {
                    ConfigureApiVersionsByConvention (options, setting);
                } else {
                    setting.ApiVersionConfigurer.Invoke (options);
                }
            }
        }

        private static void ConfigureApiVersionsByConvention (ApiVersioningOptions options, ConventionalControllerSetting setting) {
            foreach (var controllerType in setting.ControllerTypes) {
                var controllerBuilder = options.Conventions.Controller (controllerType);

                if (setting.ApiVersions.Any ()) {
                    foreach (var apiVersion in setting.ApiVersions) {
                        controllerBuilder.HasApiVersion (apiVersion);
                    }
                } else {
                    if (!controllerType.IsDefined (typeof (ApiVersionAttribute), true)) {
                        controllerBuilder.IsApiVersionNeutral ();
                    }
                }
            }
        }
    }
}