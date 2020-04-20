using System.Collections.Concurrent;

namespace System.Collections.Generic
{
    /// <summary>
    /// Dictionary <see cref="Dictionary{TKey,TValue}"/> 相关的扩展方法。
    /// </summary>
    public static class RocketDictionaryExtensions
    {
        /// <summary>
        /// 尝试从字典集合中获取值.
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="dictionary">字典集合</param>
        /// <param name="key">键</param>
        /// <param name="value">值 (字典集合中不存在则设置为默认值)</param>
        /// <returns>如果字典集合中存在值返回true</returns>
        internal static bool TryGetValue<T>(this IDictionary<string, object> dictionary, string key, out T value)
        {
            object valueObj;
            if (dictionary.TryGetValue(key, out valueObj) && valueObj is T)
            {
                value = (T)valueObj;
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// 尝试从字典集合中获取值，未查询到则返回默认值。
        /// </summary>
        /// <param name="dictionary">字典集合</param>
        /// <param name="key">键</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns>返回值，未查询到返回默认值。</returns>
        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue obj;
            return dictionary.TryGetValue(key, out obj) ? obj : default;
        }

        /// <summary>
        /// 尝试从字典集合中获取值，未查询到则返回默认值。
        /// </summary>
        /// <param name="dictionary">字典集合</param>
        /// <param name="key">键</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns>返回值，未查询到返回默认值。</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var obj) ? obj : default;
        }

        /// <summary>
        /// 尝试从字典集合中获取值，未查询到则返回默认值。
        /// </summary>
        /// <param name="dictionary">只读字典集合</param>
        /// <param name="key">键</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns>返回值，未查询到返回默认值。</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var obj) ? obj : default;
        }
        
        /// <summary>
        /// 尝试从字典集合中获取值，未查询到则返回默认值。
        /// </summary>
        /// <param name="dictionary">线程安全字典集合</param>
        /// <param name="key">键</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns>返回值，未查询到返回默认值。</returns>
        public static TValue GetOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var obj) ? obj : default;
        }

        /// <summary>
        /// 尝试从字典集合中获取值，未查询到则返回默认值。
        /// </summary>
        /// <param name="dictionary">字典集合</param>
        /// <param name="key">键</param>
        /// <param name="factory">函数工厂，如果未查询到可以对返回值赋值</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns>返回值，未查询到返回默认值（函数工厂创建）。</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
        {
            TValue obj;
            if (dictionary.TryGetValue(key, out obj))
            {
                return obj;
            }

            return dictionary[key] = factory(key);
        }
        
        /// <summary>
        /// 尝试从字典集合中获取值，未查询到则返回默认值。
        /// </summary>
        /// <param name="dictionary">字典集合</param>
        /// <param name="key">键</param>
        /// <param name="factory">函数工厂，如果未查询到可以对返回值赋值</param>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <returns>返回值，未查询到返回默认值（函数工厂创建）。</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
        {
            return dictionary.GetOrAdd(key, k => factory());
        }
    }
}