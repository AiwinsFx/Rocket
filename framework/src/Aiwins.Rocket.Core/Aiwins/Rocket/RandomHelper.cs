using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Aiwins.Rocket {
    /// <summary>
    /// Random <see cref="Random"/> 帮助类，
    /// 提供了许多实用方法.
    /// </summary>
    public static class RandomHelper {
        private static readonly Random Rnd = new Random ();

        /// <summary>
        /// 从指定的区间获取随机整数值。
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值，必须大于或者等于0</param>
        /// <returns>
        /// 随机值的范围包含最小值而不包含最大值. 
        /// 最大值和最小值相等，返回最小值。
        /// </returns>
        public static int GetRandom (int minValue, int maxValue) {
            lock (Rnd) {
                return Rnd.Next (minValue, maxValue);
            }
        }

        /// <summary>
        /// 从指定的区间（小宇给定的最大值）获取非负随机整数值。
        /// </summary>
        /// <param name="maxValue">最大值，必须大于或者等于0</param>
        /// <returns>
        /// 随机值包含0，但不包含最大值. 
        /// 但是当最大值为0，随机值返回0.
        /// </returns>
        public static int GetRandom (int maxValue) {
            lock (Rnd) {
                return Rnd.Next (maxValue);
            }
        }

        /// <summary>
        /// 获取非负随机整数值。
        /// </summary>
        /// <returns>返回 0 ~ MaxValue <see cref="int.MaxValue"/> 的随机值</returns>
        public static int GetRandom () {
            lock (Rnd) {
                return Rnd.Next ();
            }
        }

        /// <summary>
        /// 从对象数组中随机返回一个对象。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="objs">对象数组</param>
        public static T GetRandomOf<T> ([NotNull] params T[] objs) {
            Check.NotNullOrEmpty (objs, nameof (objs));

            return objs[GetRandom (0, objs.Length)];
        }

        /// <summary>
        /// 从集合中随机返回一个对象。
        /// </summary>
        /// <typeparam name="T">集合的成员类型</typeparam>
        /// <param name="list">集合</param>
        public static T GetRandomOfList<T> ([NotNull] IList<T> list) {
            Check.NotNullOrEmpty (list, nameof (list));

            return list[GetRandom (0, list.Count)];
        }

        /// <summary>
        /// 从集合中返回随机子集。
        /// </summary>
        /// <typeparam name="T">集合的成员类型</typeparam>
        /// <param name="items">集合</param>
        public static List<T> GenerateRandomizedList<T> ([NotNull] IEnumerable<T> items) {
            Check.NotNull (items, nameof (items));

            var currentList = new List<T> (items);
            var randomList = new List<T> ();

            while (currentList.Any ()) {
                var randomIndex = RandomHelper.GetRandom (0, currentList.Count);
                randomList.Add (currentList[randomIndex]);
                currentList.RemoveAt (randomIndex);
            }

            return randomList;
        }
    }
}