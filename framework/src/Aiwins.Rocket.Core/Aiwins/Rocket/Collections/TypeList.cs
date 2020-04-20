using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Aiwins.Rocket.Collections {
    /// <summary>
    /// TypeList类型 <see cref="TypeList{TBaseType}"/>。
    /// </summary>
    public class TypeList : TypeList<object>, ITypeList { }

    /// <summary>
    /// TypeList泛型类型，继承自 <see cref="List{Type}"/>。
    /// </summary>
    /// <typeparam name="TBaseType">集合成员类型 <see cref="Type"/></typeparam>
    public class TypeList<TBaseType> : ITypeList<TBaseType> {
        /// <summary>
        /// 集合数量
        /// </summary>
        /// <value>数量</value>
        public int Count => _typeList.Count;

        /// <summary>
        /// 是否为只读
        /// </summary>
        /// <value><c>true</c> 表示为只读; 否则为<c>false</c>.</value>
        public bool IsReadOnly => false;

        /// <summary>
        /// 获取或设置指定索引的类型 <see cref="Type"/> 的值。
        /// </summary>
        /// <param name="index">Index.</param>
        public Type this [int index] {
            get { return _typeList[index]; }
            set {
                CheckType (value);
                _typeList[index] = value;
            }
        }

        private readonly List<Type> _typeList;

        /// <summary>
        /// 创建集合 <see cref="TypeList{T}"/> 实例.
        /// </summary>
        public TypeList () {
            _typeList = new List<Type> ();
        }

        /// <inheritdoc/>
        public void Add<T> () where T : TBaseType {
            _typeList.Add (typeof (T));
        }

        public void TryAdd<T> () where T : TBaseType {
            if (Contains<T> ()) {
                return;
            }

            Add<T> ();
        }

        /// <inheritdoc/>
        public void Add (Type item) {
            CheckType (item);
            _typeList.Add (item);
        }

        /// <inheritdoc/>
        public void Insert (int index, Type item) {
            CheckType (item);
            _typeList.Insert (index, item);
        }

        /// <inheritdoc/>
        public int IndexOf (Type item) {
            return _typeList.IndexOf (item);
        }

        /// <inheritdoc/>
        public bool Contains<T> () where T : TBaseType {
            return Contains (typeof (T));
        }

        /// <inheritdoc/>
        public bool Contains (Type item) {
            return _typeList.Contains (item);
        }

        /// <inheritdoc/>
        public void Remove<T> () where T : TBaseType {
            _typeList.Remove (typeof (T));
        }

        /// <inheritdoc/>
        public bool Remove (Type item) {
            return _typeList.Remove (item);
        }

        /// <inheritdoc/>
        public void RemoveAt (int index) {
            _typeList.RemoveAt (index);
        }

        /// <inheritdoc/>
        public void Clear () {
            _typeList.Clear ();
        }

        /// <inheritdoc/>
        public void CopyTo (Type[] array, int arrayIndex) {
            _typeList.CopyTo (array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<Type> GetEnumerator () {
            return _typeList.GetEnumerator ();
        }

        IEnumerator IEnumerable.GetEnumerator () {
            return _typeList.GetEnumerator ();
        }

        private static void CheckType (Type item) {
            if (!typeof (TBaseType).GetTypeInfo ().IsAssignableFrom (item)) {
                throw new ArgumentException ($"Given type ({item.AssemblyQualifiedName}) should be instance of {typeof(TBaseType).AssemblyQualifiedName} ", nameof (item));
            }
        }
    }
}