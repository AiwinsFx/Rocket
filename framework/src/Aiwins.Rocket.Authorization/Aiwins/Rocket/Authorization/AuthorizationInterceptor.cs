using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.DynamicProxy;

namespace Aiwins.Rocket.Authorization {
    public class AuthorizationInterceptor : RocketInterceptor, ITransientDependency {
        private readonly IMethodInvocationAuthorizationService _methodInvocationAuthorizationService;

        public AuthorizationInterceptor (IMethodInvocationAuthorizationService methodInvocationAuthorizationService) {
            _methodInvocationAuthorizationService = methodInvocationAuthorizationService;
        }

        public override async Task InterceptAsync (IRocketMethodInvocation invocation) {
            await AuthorizeAsync (invocation);
            await invocation.ProceedAsync ();
        }

        protected virtual async Task AuthorizeAsync (IRocketMethodInvocation invocation) {
            await _methodInvocationAuthorizationService.CheckAsync (
                new MethodInvocationAuthorizationContext (
                    invocation.Method
                )
            );
        }
    }
}