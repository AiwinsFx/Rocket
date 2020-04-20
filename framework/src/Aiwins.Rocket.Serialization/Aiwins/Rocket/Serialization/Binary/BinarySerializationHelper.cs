using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Aiwins.Rocket.Serialization.Binary {
    /// <summary>
    /// 序列化/反序列化帮助类
    /// </summary>
    public static class BinarySerializationHelper {
        /// <summary>
        /// 序列化对象并返回字节数组
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <returns>字节数组</returns>
        public static byte[] Serialize (object obj) {
            using (var memoryStream = new MemoryStream ()) {
                Serialize (obj, memoryStream);
                return memoryStream.ToArray ();
            }
        }

        /// <summary>
        /// 序列化对象并存储到Stream流
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <param name="stream">stream流</param>
        /// <returns>字节数组</returns>
        public static void Serialize (object obj, Stream stream) {
            CreateBinaryFormatter ().Serialize (stream, obj);
        }

        /// <summary>
        /// 通过字节数组进行对象反序列化
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>反序列化后的对象</returns>
        public static object Deserialize (byte[] bytes) {
            using (var memoryStream = new MemoryStream (bytes)) {
                return Deserialize (memoryStream);
            }
        }

        /// <summary>
        /// 通过stream流进行对象反序列化
        /// </summary>
        /// <param name="stream">stream流</param>
        /// <returns>反序列化后的对象</returns> 
        public static object Deserialize (Stream stream) {
            return CreateBinaryFormatter ().Deserialize (stream);
        }

        /// <summary>
        /// 通过字节数组进行对象反序列化
        /// 和 <see cref="Deserialize(byte[])"/> 区别在于可以反序列化动态加载的程序集中定义的类型（如插件）
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>反序列化后的对象</returns>        
        public static object DeserializeExtended (byte[] bytes) {
            using (var memoryStream = new MemoryStream (bytes)) {
                return CreateBinaryFormatter (true).Deserialize (memoryStream);
            }
        }

        /// <summary>
        /// 通过stream流进行对象反序列化
        /// 和<see cref="Deserialize(Stream)"/> 区别在于可以反序列化动态加载的程序集中定义的类型（如插件）
        /// </summary>
        /// <param name="stream">stream流</param>
        /// <returns>反序列化后的对象</returns> 
        public static object DeserializeExtended (Stream stream) {
            return CreateBinaryFormatter (true).Deserialize (stream);
        }

        private static BinaryFormatter CreateBinaryFormatter (bool extended = false) {
            if (extended) {
            return new BinaryFormatter {
            //TODO: AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
            Binder = new ExtendedSerializationBinder ()
                };
            } else {
                return new BinaryFormatter ();
            }
        }

        /// <summary>
        /// 该类用于反序列化动态加载的程序集中定义的类型（如插件）
        /// </summary>
        private sealed class ExtendedSerializationBinder : SerializationBinder {
            public override Type BindToType (string assemblyName, string typeName) {
                var toAssemblyName = assemblyName.Split (',') [0];
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies ()) {
                    if (assembly.FullName.Split (',') [0] == toAssemblyName) {
                        return assembly.GetType (typeName);
                    }
                }

                return Type.GetType (string.Format ("{0}, {1}", typeName, assemblyName));
            }
        }
    }
}