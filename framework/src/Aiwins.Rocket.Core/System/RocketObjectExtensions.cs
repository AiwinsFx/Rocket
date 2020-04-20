using System.Globalization;
using System.Linq;

namespace System {
    /// <summary>
    /// objects <see cref="objects"/> 相关的扩展方法。
    /// </summary>
    public static class RocketObjectExtensions {
        /// <summary>
        /// 将对象转换为指定类型的对象. 
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>转换后的对象</returns>
        public static T As<T> (this object obj)
        where T : class {
            return (T) obj;
        }

        /// <summary>
        /// 通过Convert.ChangeType <see cref="Convert.ChangeType(object,System.Type)"/> 方法将对象进行转换.
        /// </summary>
        /// <param name="obj">对象</param>
        /// <typeparam name="T">指定类型</typeparam>
        /// <returns>转换后的对象</returns>
        public static T To<T> (this object obj)
        where T : struct {
            return (T) Convert.ChangeType (obj, typeof (T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 检查对象是否在数组中存在.
        /// </summary>
        /// <param name="item">对象</param>
        /// <param name="list">数组</param>
        /// <typeparam name="T">数组子项的类型</typeparam>
        public static bool IsIn<T> (this T item, params T[] list) {
            return list.Contains (item);
        }
    }
}