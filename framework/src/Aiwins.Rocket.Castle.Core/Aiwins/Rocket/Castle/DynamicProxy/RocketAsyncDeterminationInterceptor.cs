using Aiwins.Rocket.DynamicProxy;
using Castle.DynamicProxy;

namespace Aiwins.Rocket.Castle.DynamicProxy {
    public class RocketAsyncDeterminationInterceptor<TInterceptor> : AsyncDeterminationInterceptor
    where TInterceptor : IRocketInterceptor {
        public RocketAsyncDeterminationInterceptor (TInterceptor abpInterceptor) : base (new CastleAsyncRocketInterceptorAdapter<TInterceptor> (abpInterceptor)) {

        }
    }
}