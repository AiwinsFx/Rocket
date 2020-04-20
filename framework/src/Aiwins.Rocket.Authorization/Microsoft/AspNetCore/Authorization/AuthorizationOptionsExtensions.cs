using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.AspNetCore.Authorization {
    public static class AuthorizationOptionsExtensions {
        private static readonly PropertyInfo PolicyMapProperty = typeof (AuthorizationOptions)
            .GetProperty ("PolicyMap", BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// 获取应用程序所有的授权策略
        /// 
        /// 重要提示: 慎用此方法
        /// 通过反射获取配置信息 <see cref="options"/>
        /// 授权配置信息 <see cref="AuthorizationOptions"/> 变更后此方法将会进行迁移
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static List<string> GetPoliciesNames (this AuthorizationOptions options) {
            return ((IDictionary<string, AuthorizationPolicy>) PolicyMapProperty.GetValue (options)).Keys.ToList ();
        }
    }
}