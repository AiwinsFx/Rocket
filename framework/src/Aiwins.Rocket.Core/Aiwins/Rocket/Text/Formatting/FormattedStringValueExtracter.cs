using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Aiwins.Rocket.Text.Formatting {
    /// <summary>
    /// 从格式化字符串中提取动态值，
    /// 可参考 <see cref="string.Format(string,object)"/>。
    /// </summary>
    /// <example>
    /// 示例:字符串 "My name is Neo" ，字符模板 "My name is {name}"，
    /// Extract 方法执行 "Neo" as "name"。
    /// </example>
    public class FormattedStringValueExtracter {
        /// <summary>
        /// 从格式化字符串中动态提取值.
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="format">字符模板</param>
        /// <param name="ignoreCase">是否忽略大小写，默认true</param>
        public static ExtractionResult Extract (string str, string format, bool ignoreCase = false) {
            var stringComparison = ignoreCase ?
                StringComparison.OrdinalIgnoreCase :
                StringComparison.Ordinal;

            if (str == format) {
                return new ExtractionResult (true);
            }

            var formatTokens = new FormatStringTokenizer ().Tokenize (format);
            if (formatTokens.IsNullOrEmpty ()) {
                return new ExtractionResult (str == "");
            }

            var result = new ExtractionResult (true);

            for (var i = 0; i < formatTokens.Count; i++) {
                var currentToken = formatTokens[i];
                var previousToken = i > 0 ? formatTokens[i - 1] : null;

                if (currentToken.Type == FormatStringTokenType.ConstantText) {
                    if (i == 0) {
                        if (!str.StartsWith (currentToken.Text, stringComparison)) {
                            result.IsMatch = false;
                            return result;
                        }

                        str = str.Substring (currentToken.Text.Length);
                    } else {
                        var matchIndex = str.IndexOf (currentToken.Text, stringComparison);
                        if (matchIndex < 0) {
                            result.IsMatch = false;
                            return result;
                        }

                        Debug.Assert (previousToken != null, "previousToken can not be null since i > 0 here");

                        result.Matches.Add (new NameValue (previousToken.Text, str.Substring (0, matchIndex)));
                        str = str.Substring (matchIndex + currentToken.Text.Length);
                    }
                }
            }

            var lastToken = formatTokens.Last ();
            if (lastToken.Type == FormatStringTokenType.DynamicValue) {
                result.Matches.Add (new NameValue (lastToken.Text, str));
            }

            return result;
        }

        /// <summary>
        /// 检查字符串 <see cref="str"/> 时候符合给定的格式化字符串 <see cref="format"/>，
        /// 同时获取提取到的值。
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="format">字符模板</param>
        /// <param name="values">匹配到的值</param>
        /// <param name="ignoreCase">是否忽略大小写，默认true</param>
        /// <returns>匹配成功返回true</returns>
        public static bool IsMatch (string str, string format, out string[] values, bool ignoreCase = false) {
            var result = Extract (str, format, ignoreCase);
            if (!result.IsMatch) {
                values = new string[0];
                return false;
            }

            values = result.Matches.Select (m => m.Value).ToArray ();
            return true;
        }

        public class ExtractionResult {
            /// <summary>
            /// 是否完全匹配
            /// </summary>
            public bool IsMatch { get; set; }

            /// <summary>
            /// 匹配的动态值集合
            /// </summary>
            public List<NameValue> Matches { get; private set; }

            internal ExtractionResult (bool isMatch) {
                IsMatch = isMatch;
                Matches = new List<NameValue> ();
            }
        }
    }
}