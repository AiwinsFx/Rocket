using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Aiwins.Rocket;
using JetBrains.Annotations;

namespace System {
    /// <summary>
    /// string <see cref="string"/> 相关的扩展方法。
    /// </summary>
    public static class RocketStringExtensions {
        /// <summary>
        /// 如果字符串的结尾不是指定字符，则在该字符串的结尾添加一个指定的字符
        /// </summary>
        public static string EnsureEndsWith (this string str, char c, StringComparison comparisonType = StringComparison.Ordinal) {
            Check.NotNull (str, nameof (str));

            if (str.EndsWith (c.ToString (), comparisonType)) {
                return str;
            }

            return str + c;
        }

        /// <summary>
        /// 如果字符串的开头不是指定字符，则在该字符串的开头添加一个指定的字符
        /// </summary>
        public static string EnsureStartsWith (this string str, char c, StringComparison comparisonType = StringComparison.Ordinal) {
            Check.NotNull (str, nameof (str));

            if (str.StartsWith (c.ToString (), comparisonType)) {
                return str;
            }

            return c + str;
        }

        /// <summary>
        /// 判断是否为空字符串.
        /// </summary>
        public static bool IsNullOrEmpty (this string str) {
            return string.IsNullOrEmpty (str);
        }

        /// <summary>
        /// 判断是否为空字符串.
        /// </summary>
        public static bool IsNullOrWhiteSpace (this string str) {
            return string.IsNullOrWhiteSpace (str);
        }

        /// <summary>
        /// 截取字符串，保留左半边子字符串。
        /// </summary>
        /// <exception cref="ArgumentNullException">字符串 <paramref name="str"/> 为空抛出异常</exception>
        /// <exception cref="ArgumentException">字符串长度小于指定截取长度<paramref name="len"/> 抛出异常</exception>
        public static string Left (this string str, int len) {
            Check.NotNull (str, nameof (str));

            if (str.Length < len) {
                throw new ArgumentException ("len argument can not be bigger than given string's length!");
            }

            return str.Substring (0, len);
        }

        /// <summary>
        /// 将字符串中的换行符、空格替换为NewLine <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string NormalizeLineEndings (this string str) {
            return str.Replace ("\r\n", "\n").Replace ("\r", "\n").Replace ("\n", Environment.NewLine);
        }

        /// <summary>
        /// 查询指定字符在字符串中的索引位置.
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="c">查询字符 <see cref="str"/></param>
        /// <param name="n">字符的索引</param>
        public static int NthIndexOf (this string str, char c, int n) {
            Check.NotNull (str, nameof (str));

            var count = 0;
            for (var i = 0; i < str.Length; i++) {
                if (str[i] != c) {
                    continue;
                }

                if ((++count) == n) {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 从字符串的末尾移除指定后缀（可选字符数组中第一个匹配到的位置截取）。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="postFixes">可选字符数组</param>
        /// <returns>经过处理的字符串</returns>
        public static string RemovePostFix (this string str, params string[] postFixes) {
            return str.RemovePostFix (StringComparison.Ordinal, postFixes);
        }

        /// <summary>
        /// 从字符串的末尾移除指定后缀（可选字符数组中第一个匹配到的位置截取）。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="comparisonType">比较类型</param>
        /// <param name="postFixes">可选字符数组</param>
        /// <returns>经过处理的字符串</returns>
        public static string RemovePostFix (this string str, StringComparison comparisonType, params string[] postFixes) {
            if (str.IsNullOrEmpty ()) {
                return null;
            }

            if (postFixes.IsNullOrEmpty ()) {
                return str;
            }

            foreach (var postFix in postFixes) {
                if (str.EndsWith (postFix, comparisonType)) {
                    return str.Left (str.Length - postFix.Length);
                }
            }

            return str;
        }

        /// <summary>
        /// 从字符串的开头移除子字符串（可选字符数组中第一个匹配到的位置截取）。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="preFixes">可选字符数组</param>
        /// <returns>经过处理的字符串</returns>
        public static string RemovePreFix (this string str, params string[] preFixes) {
            return str.RemovePreFix (StringComparison.Ordinal, preFixes);
        }

        /// <summary>
        /// 从字符串的开头移除子字符串（可选字符数组中第一个匹配到的位置截取）。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="comparisonType">String comparison type</param>
        /// <param name="preFixes">可选字符数组</param>
        /// <returns>经过处理的字符串</returns>
        public static string RemovePreFix (this string str, StringComparison comparisonType, params string[] preFixes) {
            if (str.IsNullOrEmpty ()) {
                return null;
            }

            if (preFixes.IsNullOrEmpty ()) {
                return str;
            }

            foreach (var preFix in preFixes) {
                if (str.StartsWith (preFix, comparisonType)) {
                    return str.Right (str.Length - preFix.Length);
                }
            }

            return str;
        }

        public static string ReplaceFirst (this string str, string search, string replace, StringComparison comparisonType = StringComparison.Ordinal) {
            Check.NotNull (str, nameof (str));

            var pos = str.IndexOf (search, comparisonType);
            if (pos < 0) {
                return str;
            }

            return str.Substring (0, pos) + replace + str.Substring (pos + search.Length);
        }

        /// <summary>
        /// 截取字符串，保留右半边子字符串。
        /// </summary>
        /// <exception cref="ArgumentNullException">字符串 <paramref name="str"/> 为空抛出异常</exception>
        /// <exception cref="ArgumentException">字符串长度小于指定截取长度<paramref name="len"/> 抛出异常</exception>
        public static string Right (this string str, int len) {
            Check.NotNull (str, nameof (str));

            if (str.Length < len) {
                throw new ArgumentException ("len argument can not be bigger than given string's length!");
            }

            return str.Substring (str.Length - len, len);
        }

        /// <summary>
        /// 按照指定分隔符，通过 string.Split <see cref="string.Split"/> 方法将字符串分割生成数组。
        /// </summary>
        public static string[] Split (this string str, string separator) {
            return str.Split (new [] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// 按照指定分隔符，通过 string.Split <see cref="string.Split"/> 方法将字符串分割生成数组。
        /// </summary>
        public static string[] Split (this string str, string separator, StringSplitOptions options) {
            return str.Split (new [] { separator }, options);
        }

        /// <summary>
        /// 按照分隔符 <see cref="Environment.NewLine"/>，通过 string.Split <see cref="string.Split"/> 方法将字符串分割生成数组。
        /// </summary>
        public static string[] SplitToLines (this string str) {
            return str.Split (Environment.NewLine);
        }

        /// <summary>
        /// 按照分隔符 <see cref="Environment.NewLine"/>，通过 string.Split <see cref="string.Split"/> 方法将字符串分割生成数组。
        /// </summary>
        public static string[] SplitToLines (this string str, StringSplitOptions options) {
            return str.Split (Environment.NewLine, options);
        }

        /// <summary>
        /// PascalCase字符串转换为camelCase字符串。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="useCurrentCulture">是否使用当前区域文化</param>
        /// <returns>camelCase字符串</returns>
        public static string ToCamelCase (this string str, bool useCurrentCulture = false) {
            if (string.IsNullOrWhiteSpace (str)) {
                return str;
            }

            if (str.Length == 1) {
                return useCurrentCulture ? str.ToLower () : str.ToLowerInvariant ();
            }

            return (useCurrentCulture ? char.ToLower (str[0]) : char.ToLowerInvariant (str[0])) + str.Substring (1);
        }

        /// <summary>
        /// 将PascalCase/camelCase字符串转换为空格分割的单词。
        /// 示例: "ThisIsSampleSentence" is converted to "This is a sample sentence"。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="useCurrentCulture">是否使用当前区域文化</param>
        public static string ToSentenceCase (this string str, bool useCurrentCulture = false) {
            if (string.IsNullOrWhiteSpace (str)) {
                return str;
            }

            return useCurrentCulture ?
                Regex.Replace (str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower (m.Value[1])) :
                Regex.Replace (str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLowerInvariant (m.Value[1]));
        }

        /// <summary>
        /// 将PascalCase/camelCase字符串转换为kebab-case字符串。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="useCurrentCulture">是否使用当前区域文化</param>
        public static string ToKebabCase (this string str, bool useCurrentCulture = false) {
            if (string.IsNullOrWhiteSpace (str)) {
                return str;
            }

            str = str.ToCamelCase ();

            return useCurrentCulture ?
                Regex.Replace (str, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLower (m.Value[1])) :
                Regex.Replace (str, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLowerInvariant (m.Value[1]));
        }

        /// <summary>
        /// 字符串转换为枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">值</param>
        /// <returns>枚举值</returns>
        public static T ToEnum<T> (this string value)
        where T : struct {
            Check.NotNull (value, nameof (value));
            return (T) Enum.Parse (typeof (T), value);
        }

        /// <summary>
        /// 字符串转换为枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>枚举值</returns>
        public static T ToEnum<T> (this string value, bool ignoreCase)
        where T : struct {
            Check.NotNull (value, nameof (value));
            return (T) Enum.Parse (typeof (T), value, ignoreCase);
        }

        public static string ToMd5 (this string str) {
            using (var md5 = MD5.Create ()) {
                var inputBytes = Encoding.UTF8.GetBytes (str);
                var hashBytes = md5.ComputeHash (inputBytes);

                var sb = new StringBuilder ();
                foreach (var hashByte in hashBytes) {
                    sb.Append (hashByte.ToString ("X2"));
                }

                return sb.ToString ();
            }
        }

        /// <summary>
        /// camelCase字符串转换为PascalCase字符串。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="useCurrentCulture">是否使用当前区域文化</param>
        /// <returns>PascalCase字符串</returns>
        public static string ToPascalCase (this string str, bool useCurrentCulture = false) {
            if (string.IsNullOrWhiteSpace (str)) {
                return str;
            }

            if (str.Length == 1) {
                return useCurrentCulture ? str.ToUpper () : str.ToUpperInvariant ();
            }

            return (useCurrentCulture ? char.ToUpper (str[0]) : char.ToUpperInvariant (str[0])) + str.Substring (1);
        }

        /// <summary>
        /// 截取指定长度的字符串，保留左边子字符串
        /// </summary>
        /// <exception cref="ArgumentNullException">字符串为空 <paramref name="str"/> 抛出异常</exception>
        public static string Truncate (this string str, int maxLength) {
            if (str == null) {
                return null;
            }

            if (str.Length <= maxLength) {
                return str;
            }

            return str.Left (maxLength);
        }

        /// <summary>
        /// 截取指定长度的字符串，保留右边子字符串
        /// </summary>
        /// <exception cref="ArgumentNullException">字符串为空 <paramref name="str"/> 抛出异常</exception>
        public static string TruncateFromBeginning (this string str, int maxLength) {
            if (str == null) {
                return null;
            }

            if (str.Length <= maxLength) {
                return str;
            }

            return str.Right (maxLength);
        }

        /// <summary>
        /// 截取指定长度字符串，超出部分添加字符串后缀...
        /// </summary>
        /// <exception cref="ArgumentNullException">字符串为空 <paramref name="str"/> 抛出异常</exception>
        public static string TruncateWithPostfix (this string str, int maxLength) {
            return TruncateWithPostfix (str, maxLength, "...");
        }

        /// <summary>
        /// 截取指定长度字符串，超出部分添加后缀
        /// </summary>
        /// <exception cref="ArgumentNullException">字符串为空 <paramref name="str"/> 抛出异常</exception>
        public static string TruncateWithPostfix (this string str, int maxLength, string postfix) {
            if (str == null) {
                return null;
            }

            if (str == string.Empty || maxLength == 0) {
                return string.Empty;
            }

            if (str.Length <= maxLength) {
                return str;
            }

            if (maxLength <= postfix.Length) {
                return postfix.Left (maxLength);
            }

            return str.Left (maxLength - postfix.Length) + postfix;
        }

        /// <summary>
        /// 通过UTF8编码格式将字符串转换为字节数组。
        /// </summary>
        public static byte[] GetBytes (this string str) {
            return str.GetBytes (Encoding.UTF8);
        }

        /// <summary>
        /// 通过指定编码格式将字符串转换为字节数组。
        /// </summary>
        public static byte[] GetBytes ([NotNull] this string str, [NotNull] Encoding encoding) {
            Check.NotNull (str, nameof (str));
            Check.NotNull (encoding, nameof (encoding));

            return encoding.GetBytes (str);
        }
    }
}