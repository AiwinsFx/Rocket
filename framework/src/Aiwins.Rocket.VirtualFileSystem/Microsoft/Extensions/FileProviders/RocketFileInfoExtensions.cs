using JetBrains.Annotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Aiwins.Rocket;
using Aiwins.Rocket.VirtualFileSystem;
using Aiwins.Rocket.VirtualFileSystem.Embedded;

namespace Microsoft.Extensions.FileProviders
{
    public static class RocketFileInfoExtensions
    {
        /// <summary>
        /// 以UTF8编码格式<see cref="Encoding.UTF8"/> 读取文件内容。
        /// </summary>
        public static string ReadAsString([NotNull] this IFileInfo fileInfo)
        {
            return fileInfo.ReadAsString(Encoding.UTF8);
        }

        /// <summary>
        /// 以指定编码格式 <paramref name="encoding"/> 读取文件内容。
        /// </summary>
        public static string ReadAsString([NotNull] this IFileInfo fileInfo, Encoding encoding)
        {
            Check.NotNull(fileInfo, nameof(fileInfo));

            using (var stream = fileInfo.CreateReadStream())
            {
                using (var streamReader = new StreamReader(stream, encoding, true))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 读取文件内容并转化为字节数组
        /// </summary>
        public static byte[] ReadBytes([NotNull] this IFileInfo fileInfo)
        {
            Check.NotNull(fileInfo, nameof(fileInfo));

            using (var stream = fileInfo.CreateReadStream())
            {
                return stream.GetAllBytes();
            }
        }

        /// <summary>
        /// 读取文件内容并转化为字节数组
        /// </summary>
        public static async Task<byte[]> ReadBytesAsync([NotNull] this IFileInfo fileInfo)
        {
            Check.NotNull(fileInfo, nameof(fileInfo));

            using (var stream = fileInfo.CreateReadStream())
            {
                return await stream.GetAllBytesAsync();
            }
        }

        public static string GetVirtualOrPhysicalPathOrNull([NotNull] this IFileInfo fileInfo)
        {
            Check.NotNull(fileInfo, nameof(fileInfo));

            if (fileInfo is EmbeddedResourceFileInfo embeddedFileInfo)
            {
                return embeddedFileInfo.VirtualPath;
            }

            if (fileInfo is InMemoryFileInfo inMemoryFileInfo)
            {
                return inMemoryFileInfo.DynamicPath;
            }

            return fileInfo.PhysicalPath;
        }
    }
}
