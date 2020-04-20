using System.Reflection;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Uow {
    public static class UnitOfWorkInterceptorRegistrar {
        public static void RegisterIfNeeded (IOnServiceRegistredContext context) {
            if (UnitOfWorkHelper.IsUnitOfWorkType (context.ImplementationType.GetTypeInfo ())) {
                context.Interceptors.TryAdd<UnitOfWorkInterceptor> ();
            }
        }
    }
}