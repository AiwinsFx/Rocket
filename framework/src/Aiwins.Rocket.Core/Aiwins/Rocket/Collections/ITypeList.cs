using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.Collections {
    /// <summary>
    /// ITypeList接口 <see cref="ITypeList{TBaseType}"/>。
    /// </summary>
    public interface ITypeList : ITypeList<object> {

    }

    /// <summary>
    /// ITypeList泛型接口，继承自 <see cref="IList{Type}"/>。
    /// </summary>
    /// <typeparam name="TBaseType">集合成员类型 <see cref="Type"/></typeparam>
    public interface ITypeList<in TBaseType> : IList<Type> {
        /// <summary>
        /// 添加指定类型至集合。
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        void Add<T> () where T : TBaseType;

        /// <summary>
        /// 如果集合中不包含指定类型，则添加至集合。
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        void TryAdd<T> () where T : TBaseType;

        /// <summary>
        /// 检查集合中是否包含指定类型。
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <returns></returns>
        bool Contains<T> () where T : TBaseType;

        /// <summary>
        /// 从集合中移除类型。
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        void Remove<T> () where T : TBaseType;
    }
}