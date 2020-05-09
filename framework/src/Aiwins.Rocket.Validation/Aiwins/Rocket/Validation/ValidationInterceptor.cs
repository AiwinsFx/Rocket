using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.DynamicProxy;

namespace Aiwins.Rocket.Validation {
    public class ValidationInterceptor : RocketInterceptor, ITransientDependency {
        private readonly IMethodInvocationValidator _methodInvocationValidator;

        public ValidationInterceptor (IMethodInvocationValidator methodInvocationValidator) {
            _methodInvocationValidator = methodInvocationValidator;
        }

        public override async Task InterceptAsync (IRocketMethodInvocation invocation) {
            Validate (invocation);
            await invocation.ProceedAsync ();
        }

        protected virtual void Validate (IRocketMethodInvocation invocation) {
            _methodInvocationValidator.Validate (
                new MethodInvocationValidationContext (
                    invocation.TargetObject,
                    invocation.Method,
                    invocation.Arguments
                )
            );
        }
    }
}