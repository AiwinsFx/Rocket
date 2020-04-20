using System.Linq;

namespace System.Reflection {
    /// <summary>
    /// MemberInfo <see cref="MemberInfo"/> 相关的扩展方法
    /// </summary>
    public static class RocketMemberInfoExtensions {
        /// <summary>
        /// 获取方法的特性 Attribute.
        /// </summary>
        /// <typeparam name="TAttribute">特性的类型</typeparam>
        /// <param name="memberInfo">成员信息</param>
        /// <param name="inherit">是否包含继承的特性</param>
        /// <returns>返回查询到的Attribute，未查询到返回Null</returns>
        public static TAttribute GetSingleAttributeOrNull<TAttribute> (this MemberInfo memberInfo, bool inherit = true)
        where TAttribute : Attribute {
            if (memberInfo == null) {
                throw new ArgumentNullException (nameof (memberInfo));
            }

            var attrs = memberInfo.GetCustomAttributes (typeof (TAttribute), inherit).ToArray ();
            if (attrs.Length > 0) {
                return (TAttribute) attrs[0];
            }

            return default;
        }

        public static TAttribute GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute> (this Type type, bool inherit = true)
        where TAttribute : Attribute {
            var attr = type.GetTypeInfo ().GetSingleAttributeOrNull<TAttribute> ();
            if (attr != null) {
                return attr;
            }

            if (type.GetTypeInfo ().BaseType == null) {
                return null;
            }

            return type.GetTypeInfo ().BaseType.GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute> (inherit);
        }
    }
}