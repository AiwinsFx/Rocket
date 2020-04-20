using System.Threading.Tasks;

namespace Aiwins.Rocket.DynamicProxy {
    public abstract class RocketInterceptor : IRocketInterceptor {
        public abstract Task InterceptAsync (IRocketMethodInvocation invocation);
    }
}