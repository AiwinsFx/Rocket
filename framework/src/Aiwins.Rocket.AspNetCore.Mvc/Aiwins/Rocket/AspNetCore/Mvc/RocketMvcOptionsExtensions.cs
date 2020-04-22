using Aiwins.Rocket.AspNetCore.Mvc.Auditing;
using Aiwins.Rocket.AspNetCore.Mvc.Conventions;
using Aiwins.Rocket.AspNetCore.Mvc.ExceptionHandling;
using Aiwins.Rocket.AspNetCore.Mvc.Features;
using Aiwins.Rocket.AspNetCore.Mvc.ModelBinding;
using Aiwins.Rocket.AspNetCore.Mvc.Response;
using Aiwins.Rocket.AspNetCore.Mvc.Uow;
using Aiwins.Rocket.AspNetCore.Mvc.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc {
    internal static class RocketMvcOptionsExtensions {
        public static void AddRocket (this MvcOptions options, IServiceCollection services) {
            AddConventions (options, services);
            AddActionFilters (options);
            AddPageFilters (options);
            AddModelBinders (options);
            AddMetadataProviders (options, services);
        }

        private static void AddConventions (MvcOptions options, IServiceCollection services) {
            options.Conventions.Add (new RocketServiceConventionWrapper (services));
        }

        private static void AddActionFilters (MvcOptions options) {
            options.Filters.AddService (typeof (RocketAuditActionFilter));
            options.Filters.AddService (typeof (RocketNoContentActionFilter));
            options.Filters.AddService (typeof (RocketFeatureActionFilter));
            options.Filters.AddService (typeof (RocketValidationActionFilter));
            options.Filters.AddService (typeof (RocketUowActionFilter));
            options.Filters.AddService (typeof (RocketExceptionFilter));
        }

        private static void AddPageFilters (MvcOptions options) {
            options.Filters.AddService (typeof (RocketExceptionPageFilter));
            options.Filters.AddService (typeof (RocketAuditPageFilter));
            options.Filters.AddService (typeof (RocketFeaturePageFilter));
            options.Filters.AddService (typeof (RocketUowPageFilter));
        }

        private static void AddModelBinders (MvcOptions options) {
            options.ModelBinderProviders.Insert (0, new RocketDateTimeModelBinderProvider ());
        }

        private static void AddMetadataProviders (MvcOptions options, IServiceCollection services) {
            options.ModelMetadataDetailsProviders.Add (
                new RocketDataAnnotationAutoLocalizationMetadataDetailsProvider (services)
            );
        }
    }
}