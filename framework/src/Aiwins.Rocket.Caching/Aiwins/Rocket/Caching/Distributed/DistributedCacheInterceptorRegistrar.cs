using System;
using System.Linq;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.DynamicProxy;

namespace Aiwins.Rocket.Caching {
    public static class DistributedCacheInterceptorRegistrar {
        public static void RegisterIfNeeded (IOnServiceRegistredContext context) {
            if (ShouldIntercept (context.ImplementationType)) {
                context.Interceptors.TryAdd<DistributedCacheInterceptor> ();
            }
        }

        private static bool ShouldIntercept (Type type) {
            if (DynamicProxyIgnoreTypes.Contains (type)) {
                return false;
            }

            if (type.GetMethods ().Any (m => m.IsDefined (typeof (DistributedCacheAttribute), true))) {
                return true;
            }

            return false;
        }
    }
}