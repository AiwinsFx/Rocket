using System.Text;

namespace Aiwins.Rocket.Text {
    public class StringHelper {
        /// <summary>
        /// 将字节数组转换为不带BOM（子节顺序标记）的字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="encoding">编码方式，默认UTF8</param>
        /// <returns></returns>
        public static string ConvertFromBytesWithoutBom (byte[] bytes, Encoding encoding = null) {
            if (bytes == null) {
                return null;
            }

            if (encoding == null) {
                encoding = Encoding.UTF8;
            }

            var hasBom = bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;

            if (hasBom) {
                return encoding.GetString (bytes, 3, bytes.Length - 3);
            } else {
                return encoding.GetString (bytes);
            }
        }
    }
}