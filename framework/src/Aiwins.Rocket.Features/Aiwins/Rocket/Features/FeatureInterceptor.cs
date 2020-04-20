using System.Threading.Tasks;
using Aiwins.Rocket.Aspects;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.DynamicProxy;

namespace Aiwins.Rocket.Features
{
    public class FeatureInterceptor : RocketInterceptor, ITransientDependency
    {
        private readonly IMethodInvocationFeatureCheckerService _methodInvocationFeatureCheckerService;

        public FeatureInterceptor(
            IMethodInvocationFeatureCheckerService methodInvocationFeatureCheckerService)
        {
            _methodInvocationFeatureCheckerService = methodInvocationFeatureCheckerService;
        }

        public override async Task InterceptAsync(IRocketMethodInvocation invocation)
        {
            if (RocketCrossCuttingConcerns.IsApplied(invocation.TargetObject, RocketCrossCuttingConcerns.FeatureChecking))
            {
                await invocation.ProceedAsync();
                return;
            }

            await CheckFeaturesAsync(invocation);
            await invocation.ProceedAsync();
        }

        protected virtual async Task CheckFeaturesAsync(IRocketMethodInvocation invocation)
        {
            await _methodInvocationFeatureCheckerService.CheckAsync(
                new MethodInvocationFeatureCheckerContext(
                    invocation.Method
                )
            );
        }
    }
}
