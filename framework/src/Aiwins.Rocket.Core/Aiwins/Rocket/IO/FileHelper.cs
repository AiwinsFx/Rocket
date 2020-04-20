using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Aiwins.Rocket.Text;
using JetBrains.Annotations;

namespace Aiwins.Rocket.IO {
    /// <summary>
    /// File 帮助类
    /// </summary>
    public static class FileHelper {
        /// <summary>
        /// 检查并删除指定的文件（如果文件存在）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void DeleteIfExists (string filePath) {
            if (File.Exists (filePath)) {
                File.Delete (filePath);
            }
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="fileNameWithExtension">文件名</param>
        /// <returns>
        /// 返回文件扩展名，不包含"."
        /// 如果文件名 <paramref name="fileNameWithExtension"></paramref> 不包含"."，则返回空值。
        /// </returns>
        [CanBeNull]
        public static string GetExtension ([NotNull] string fileNameWithExtension) {
            Check.NotNull (fileNameWithExtension, nameof (fileNameWithExtension));

            var lastDotIndex = fileNameWithExtension.LastIndexOf ('.');
            if (lastDotIndex < 0) {
                return null;
            }

            return fileNameWithExtension.Substring (lastDotIndex + 1);
        }

        /// <summary>
        /// 读取文件并以字符串的形式返回文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件内容</returns>
        public static async Task<string> ReadAllTextAsync (string path) {
            using (var reader = File.OpenText (path)) {
                return await reader.ReadToEndAsync ();
            }
        }

        /// <summary>
        /// 读取文件并以字节数组的形式返回文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件内容</returns>
        public static async Task<byte[]> ReadAllBytesAsync (string path) {
            using (var stream = File.Open (path, FileMode.Open)) {
                var result = new byte[stream.Length];
                await stream.ReadAsync (result, 0, (int) stream.Length);
                return result;
            }
        }

        /// <summary>
        /// 读取文件，并以字符串数组的形式返回文件的所有行内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">文件编码方式，默认为UTF8</param>
        /// <param name="fileMode">指定文件打开方式，默认为FileMode.Open</param>
        /// <param name="fileAccess">指定文件访问权限，默认为FileAccess.Read</param>
        /// <param name="fileShare">指定文件共享锁，默认为FileShare.Read</param>
        /// <param name="bufferSize">指定缓冲区的大小</param>
        /// <param name="fileOptions">文件配置，默认值为Asynchronous（异步读取）和SequentialScan（文件按照从头到尾按顺序读取）</param>
        /// <returns>文件内容</returns>
        public static async Task<string[]> ReadAllLinesAsync (string path,
            Encoding encoding = null,
            FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read,
            FileShare fileShare = FileShare.Read,
            int bufferSize = 4096,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan) {
            if (encoding == null) {
                encoding = Encoding.UTF8;
            }

            var lines = new List<string> ();

            using (var stream = new FileStream (
                path,
                fileMode,
                fileAccess,
                fileShare,
                bufferSize,
                fileOptions)) {
                using (var reader = new StreamReader (stream, encoding)) {
                    string line;
                    while ((line = await reader.ReadLineAsync ()) != null) {
                        lines.Add (line);
                    }
                }
            }

            return lines.ToArray ();
        }

        /// <summary>
        /// 读取文件并以不带BOM（子节顺序标记）的字符串的形式返回文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件内容</returns>
        public static async Task<string> ReadFileWithoutBomAsync (string path) {
            var content = await ReadAllBytesAsync (path);

            return StringHelper.ConvertFromBytesWithoutBom (content);
        }
    }
}