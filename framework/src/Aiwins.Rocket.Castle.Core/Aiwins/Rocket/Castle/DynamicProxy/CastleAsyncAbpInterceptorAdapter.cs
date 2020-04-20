using System;
using System.Threading.Tasks;
using Aiwins.Rocket.DynamicProxy;
using Castle.DynamicProxy;

namespace Aiwins.Rocket.Castle.DynamicProxy {
    public class CastleAsyncRocketInterceptorAdapter<TInterceptor> : AsyncInterceptorBase
    where TInterceptor : IRocketInterceptor {
        private readonly TInterceptor _rocketInterceptor;

        public CastleAsyncRocketInterceptorAdapter (TInterceptor rocketInterceptor) {
            _rocketInterceptor = rocketInterceptor;
        }

        protected override async Task InterceptAsync (IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed) {
            await _rocketInterceptor.InterceptAsync (
                new CastleRocketMethodInvocationAdapter (invocation, proceedInfo, proceed)
            );
        }

        protected override async Task<TResult> InterceptAsync<TResult> (IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed) {
            var adapter = new CastleRocketMethodInvocationAdapterWithReturnValue<TResult> (invocation, proceedInfo, proceed);

            await _rocketInterceptor.InterceptAsync (
                adapter
            );

            return (TResult) adapter.ReturnValue;
        }
    }
}