using System.Threading.Tasks;

namespace Aiwins.Rocket.DynamicProxy {
    public interface IRocketInterceptor {
        Task InterceptAsync (IRocketMethodInvocation invocation);
    }
}