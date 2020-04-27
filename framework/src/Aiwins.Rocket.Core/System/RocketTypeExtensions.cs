using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Aiwins.Rocket;
using JetBrains.Annotations;

namespace System {
    public static class RocketTypeExtensions {
        private static readonly ConcurrentDictionary<TypeInfo, bool> isTaskOfTCache = new ConcurrentDictionary<TypeInfo, bool> ();
        private static readonly ConcurrentDictionary<TypeInfo, bool> isValueTaskOfTCache = new ConcurrentDictionary<TypeInfo, bool> ();

        public static string GetFullNameWithAssemblyName (this Type type) {
            return type.FullName + ", " + type.Assembly.GetName ().Name;
        }

        /// <summary>
        /// 通过 <see cref="Type.IsAssignableFrom"/> 方法判断类是否实现了指定类型 <typeparamref name="TTarget"></typeparamref>.
        /// </summary>
        /// <typeparam name="TTarget">指定类型</typeparam>
        public static bool IsAssignableTo<TTarget> ([NotNull] this Type type) {
            Check.NotNull (type, nameof (type));

            return type.IsAssignableTo (typeof (TTarget));
        }

        /// <summary>
        /// 通过 <see cref="Type.IsAssignableFrom"/> 方法判断类是否实现了指定类型 <typeparamref name="TTarget"></typeparamref>.
        /// </summary>
        /// <typeparam name="TTarget">指定类型</typeparam>
        public static bool IsAssignableTo ([NotNull] this Type type, [NotNull] Type targetType) {
            Check.NotNull (type, nameof (type));
            Check.NotNull (targetType, nameof (targetType));

            return targetType.IsAssignableFrom (type);
        }

        /// <summary>
        /// 获取类型的所有基类。
        /// </summary>
        /// <param name="includeObject">返回的基类数组中是否包含object <see cref="object"/> ,默认为true </param>
        public static Type[] GetBaseClasses ([NotNull] this Type type, bool includeObject = true) {
            Check.NotNull (type, nameof (type));

            var types = new List<Type> ();
            AddTypeAndBaseTypesRecursively (types, type.BaseType, includeObject);
            return types.ToArray ();
        }

        private static void AddTypeAndBaseTypesRecursively (
            [NotNull] List<Type> types, [CanBeNull] Type type,
            bool includeObject) {
            Check.NotNull (types, nameof (types));

            if (type == null) {
                return;
            }

            if (!includeObject && type == typeof (object)) {
                return;
            }

            AddTypeAndBaseTypesRecursively (types, type.BaseType, includeObject);
            types.Add (type);
        }

        public static bool IsTask (this TypeInfo typeInfo) {
            if (typeInfo == null) {
                throw new ArgumentNullException (nameof (typeInfo));
            }
            return typeInfo.AsType () == typeof (Task);
        }

        public static bool IsTaskWithResult (this TypeInfo typeInfo) {
            if (typeInfo == null) {
                throw new ArgumentNullException (nameof (typeInfo));
            }
            return isTaskOfTCache.GetOrAdd (typeInfo, Info => Info.IsGenericType && typeof (Task).GetTypeInfo ().IsAssignableFrom (Info));
        }

        public static bool IsValueTask (this TypeInfo typeInfo) {
            if (typeInfo == null) {
                throw new ArgumentNullException (nameof (typeInfo));
            }
            return typeInfo.AsType () == typeof (ValueTask);
        }

        public static bool IsValueTaskWithResult (this TypeInfo typeInfo) {
            if (typeInfo == null) {
                throw new ArgumentNullException (nameof (typeInfo));
            }
            return isValueTaskOfTCache.GetOrAdd (typeInfo, Info => Info.IsGenericType && Info.GetGenericTypeDefinition () == typeof (ValueTask<>));
        }
    }
}