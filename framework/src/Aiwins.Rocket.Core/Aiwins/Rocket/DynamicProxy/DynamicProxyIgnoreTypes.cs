using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiwins.Rocket.DynamicProxy {
    /// <summary>
    /// Castle的动态代理类特性会对一些组件产生性能问题，例如Asp-net-core-MVC的控制器。
    /// 有关讨论，请参见：https://github.com/castleproject/Core/issues/486 https://github.com/aiwinsfx/rocket/issues/3180
    /// Rocket框架可以为某些需要动态代理类的组件（UOW、审计、授权等）启用拦截器，但会导致应用程序性能下降。
    /// 我们需要为控制器使用其他方法来实现拦截，例如中间件或MVC/页面过滤器。
    /// 因此，我们提供一些被忽略的类型以避免启用动态代理类。
    /// 默认情况下为空。在应用程序中为这些组件使用中间件或筛选器时，可以将这些类型添加到列表中。
    /// </summary>
    public static class DynamicProxyIgnoreTypes {
        private static HashSet<Type> IgnoredTypes { get; } = new HashSet<Type> ();

        public static void Add<T> () {
            lock (IgnoredTypes) {
                IgnoredTypes.AddIfNotContains (typeof (T));
            }
        }

        public static bool Contains (Type type, bool includeDerivedTypes = true) {
            lock (IgnoredTypes) {
                return includeDerivedTypes ?
                    IgnoredTypes.Any (t => t.IsAssignableFrom (type)) :
                    IgnoredTypes.Contains (type);
            }
        }
    }
}