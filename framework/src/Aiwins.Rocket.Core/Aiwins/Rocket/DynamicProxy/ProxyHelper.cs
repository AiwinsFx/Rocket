using System.Linq;
using System.Reflection;

namespace Aiwins.Rocket.DynamicProxy {
    public static class ProxyHelper {
        private const string ProxyNamespace = "Castle.Proxies";
        /// <summary>
        /// 如果指定实例为代理对象，则返回动态代理的目标对象，否则返回实例. 
        /// 支持Castle框架动态代理.
        /// </summary>
        public static object UnProxy (object obj) {
            if (obj.GetType ().Namespace != ProxyNamespace) {
                return obj;
            }

            var targetField = obj.GetType ()
                .GetFields (BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault (f => f.Name == "__target");

            if (targetField == null) {
                return obj;
            }

            return targetField.GetValue (obj);
        }
    }
}