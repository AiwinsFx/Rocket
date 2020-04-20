using System;

namespace Aiwins.Rocket {
    /// <summary>
    /// 可用于存储 Name/Value (或者 Key/Value) 对象。
    /// </summary>
    [Serializable]
    public class NameValue : NameValue<string> {
        public NameValue () {

        }

        public NameValue (string name, string value) {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    /// 可用于存储 Name/Value (或者 Key/Value) 对象。
    /// </summary>
    [Serializable]
    public class NameValue<T> {
        /// <summary>
        /// 名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值.
        /// </summary>
        public T Value { get; set; }

        public NameValue () {

        }

        public NameValue (string name, T value) {
            Name = name;
            Value = value;
        }
    }
}