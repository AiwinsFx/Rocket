using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aiwins.Rocket.Reflection {
    //TODO: 考虑为内部帮助类
    public static class ReflectionHelper {
        /// <summary>
        /// 检查给定的类型 <paramref name="givenType"/> 是否实现了泛型类 <paramref name="genericType"/>.
        /// </summary>
        /// <param name="givenType">给定类型</param>
        /// <param name="genericType">泛型类</param>
        public static bool IsAssignableToGenericType (Type givenType, Type genericType) {
            var givenTypeInfo = givenType.GetTypeInfo ();

            if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition () == genericType) {
                return true;
            }

            foreach (var interfaceType in givenTypeInfo.GetInterfaces ()) {
                if (interfaceType.GetTypeInfo ().IsGenericType && interfaceType.GetGenericTypeDefinition () == genericType) {
                    return true;
                }
            }

            if (givenTypeInfo.BaseType == null) {
                return false;
            }

            return IsAssignableToGenericType (givenTypeInfo.BaseType, genericType);
        }

        //TODO: 摘要
        public static List<Type> GetImplementedGenericTypes (Type givenType, Type genericType) {
            var result = new List<Type> ();
            AddImplementedGenericTypes (result, givenType, genericType);
            return result;
        }

        private static void AddImplementedGenericTypes (List<Type> result, Type givenType, Type genericType) {
            var givenTypeInfo = givenType.GetTypeInfo ();

            if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition () == genericType) {
                result.AddIfNotContains (givenType);
            }

            foreach (var interfaceType in givenTypeInfo.GetInterfaces ()) {
                if (interfaceType.GetTypeInfo ().IsGenericType && interfaceType.GetGenericTypeDefinition () == genericType) {
                    result.AddIfNotContains (interfaceType);
                }
            }

            if (givenTypeInfo.BaseType == null) {
                return;
            }

            AddImplementedGenericTypes (result, givenTypeInfo.BaseType, genericType);
        }

        /// <summary>
        /// 获取类成员的特性信息
        /// 如果类没有声明，则返回默认值
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="memberInfo">成员信息</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="inherit">是否获取基类的特性</param>
        public static TAttribute GetSingleAttributeOrDefault<TAttribute> (MemberInfo memberInfo, TAttribute defaultValue = default, bool inherit = true)
        where TAttribute : Attribute {
            if (memberInfo.IsDefined (typeof (TAttribute), inherit)) {
                return memberInfo.GetCustomAttributes (typeof (TAttribute), inherit).Cast<TAttribute> ().First ();
            }

            return defaultValue;
        }

        /// <summary>
        /// 获取类成员的特性信息
        /// 如果类没有声明，则返回默认值
        /// </summary>
        /// <typeparam name="TAttribute">特性类型</typeparam>
        /// <param name="memberInfo">成员信息</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="inherit">是否获取基类的特性</param>
        public static TAttribute GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<TAttribute> (MemberInfo memberInfo, TAttribute defaultValue = default, bool inherit = true)
        where TAttribute : class {
            return memberInfo.GetCustomAttributes (true).OfType<TAttribute> ().FirstOrDefault () ??
                memberInfo.DeclaringType?.GetTypeInfo ().GetCustomAttributes (true).OfType<TAttribute> ().FirstOrDefault () ??
                defaultValue;
        }

        /// <summary>
        /// 获取给定对象的属性值
        /// </summary>
        public static object GetValueByPath (object obj, Type objectType, string propertyPath) {
            var value = obj;
            var currentType = objectType;
            var objectPath = currentType.FullName;
            var absolutePropertyPath = propertyPath;
            if (absolutePropertyPath.StartsWith (objectPath)) {
                absolutePropertyPath = absolutePropertyPath.Replace (objectPath + ".", "");
            }

            foreach (var propertyName in absolutePropertyPath.Split ('.')) {
                var property = currentType.GetProperty (propertyName);
                value = property.GetValue (value, null);
                currentType = property.PropertyType;
            }

            return value;
        }

        /// <summary>
        /// 设置给定对象的属性值
        /// </summary>
        internal static void SetValueByPath (object obj, Type objectType, string propertyPath, object value) {
            var currentType = objectType;
            PropertyInfo property;
            var objectPath = currentType.FullName;
            var absolutePropertyPath = propertyPath;
            if (absolutePropertyPath.StartsWith (objectPath)) {
                absolutePropertyPath = absolutePropertyPath.Replace (objectPath + ".", "");
            }

            var properties = absolutePropertyPath.Split ('.');

            if (properties.Length == 1) {
                property = objectType.GetProperty (properties.First ());
                property.SetValue (obj, value);
                return;
            }

            for (int i = 0; i < properties.Length - 1; i++) {
                property = currentType.GetProperty (properties[i]);
                obj = property.GetValue (obj, null);
                currentType = property.PropertyType;
            }

            property = currentType.GetProperty (properties.Last ());
            property.SetValue (obj, value);
        }

        /// <summary>
        /// 获取指定类型中的所有常量值（包括基类中的常量）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string[] GetPublicConstantsRecursively (Type type) {
            const int maxRecursiveParameterValidationDepth = 8;

            var publicConstants = new List<string> ();

            void Recursively (List<string> constants, Type targetType, int currentDepth) {
                if (currentDepth > maxRecursiveParameterValidationDepth) {
                    return;
                }

                constants.AddRange (targetType.GetFields (BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Where (x => x.IsLiteral && !x.IsInitOnly)
                    .Select (x => x.GetValue (null).ToString ()));

                var nestedTypes = targetType.GetNestedTypes (BindingFlags.Public);

                foreach (var nestedType in nestedTypes) {
                    Recursively (constants, nestedType, currentDepth + 1);
                }
            }

            Recursively (publicConstants, type, 1);

            return publicConstants.ToArray ();
        }
    }
}