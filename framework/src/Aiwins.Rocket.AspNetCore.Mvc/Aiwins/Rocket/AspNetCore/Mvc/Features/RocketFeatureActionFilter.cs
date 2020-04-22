using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Aspects;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Features;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aiwins.Rocket.AspNetCore.Mvc.Features {
    public class RocketFeatureActionFilter : IAsyncActionFilter, ITransientDependency {
        private readonly IMethodInvocationFeatureCheckerService _methodInvocationAuthorizationService;

        public RocketFeatureActionFilter (IMethodInvocationFeatureCheckerService methodInvocationAuthorizationService) {
            _methodInvocationAuthorizationService = methodInvocationAuthorizationService;
        }

        public async Task OnActionExecutionAsync (
            ActionExecutingContext context,
            ActionExecutionDelegate next) {
            if (!context.ActionDescriptor.IsControllerAction ()) {
                await next ();
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo ();

            using (RocketCrossCuttingConcerns.Applying (context.Controller, RocketCrossCuttingConcerns.FeatureChecking)) {
                await _methodInvocationAuthorizationService.CheckAsync (
                    new MethodInvocationFeatureCheckerContext (methodInfo)
                );

                await next ();
            }
        }
    }
}