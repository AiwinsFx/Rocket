using System;
using System.Linq;
using System.Reflection;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.DynamicProxy;

namespace Aiwins.Rocket.Features
{
    public static class FeatureInterceptorRegistrar
    {
        public static void RegisterIfNeeded(IOnServiceRegistredContext context)
        {
            if (ShouldIntercept(context.ImplementationType))
            {
                context.Interceptors.TryAdd<FeatureInterceptor>();
            }
        }

        private static bool ShouldIntercept(Type type)
        {
            return !DynamicProxyIgnoreTypes.Contains(type) &&
                   (type.IsDefined(typeof(RequiresFeatureAttribute), true) ||
                    AnyMethodHasRequiresFeatureAttribute(type));
        }

        private static bool AnyMethodHasRequiresFeatureAttribute(Type implementationType)
        {
            return implementationType
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Any(HasRequiresFeatureAttribute);
        }

        private static bool HasRequiresFeatureAttribute(MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(RequiresFeatureAttribute), true);
        }
    }
}