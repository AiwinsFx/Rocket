using System.Threading.Tasks;
using Aiwins.Rocket.Aspects;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Features;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aiwins.Rocket.AspNetCore.Mvc.Features {
    public class RocketFeaturePageFilter : IAsyncPageFilter, ITransientDependency {
        private readonly IMethodInvocationFeatureCheckerService _methodInvocationAuthorizationService;

        public RocketFeaturePageFilter (IMethodInvocationFeatureCheckerService methodInvocationAuthorizationService) {
            _methodInvocationAuthorizationService = methodInvocationAuthorizationService;
        }

        public Task OnPageHandlerSelectionAsync (PageHandlerSelectedContext context) {
            return Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync (PageHandlerExecutingContext context, PageHandlerExecutionDelegate next) {
            if (context.HandlerMethod == null || !context.ActionDescriptor.IsPageAction ()) {
                await next ();
                return;
            }

            var methodInfo = context.HandlerMethod.MethodInfo;

            using (RocketCrossCuttingConcerns.Applying (context.HandlerInstance, RocketCrossCuttingConcerns.FeatureChecking)) {
                await _methodInvocationAuthorizationService.CheckAsync (
                    new MethodInvocationFeatureCheckerContext (methodInfo)
                );

                await next ();
            }
        }
    }
}