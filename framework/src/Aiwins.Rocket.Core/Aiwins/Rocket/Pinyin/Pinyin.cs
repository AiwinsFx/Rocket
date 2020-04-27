using System;
using System.Text;

namespace Aiwins.Rocket.Pinyin {
    public static class Pinyin {
        /// <summary>
        /// 取中文文本的拼音首字母
        /// </summary>
        /// <param name="text">编码为UTF8的文本</param>
        /// <returns>返回中文对应的拼音首字母</returns>
        public static string GetFirstPySpelling (string text) {
            if (text.IsNullOrEmpty ())
                return null;
            text = text.Trim ();
            StringBuilder chars = new StringBuilder ();
            for (var i = 0; i < text.Length; ++i) {
                string py = GetPySpelling (text[i]);
                if (py != string.Empty) chars.Append (py[0]);
            }

            return chars.ToString ().ToUpper ();
        }

        /// <summary>
        /// 取中文文本的拼音首字母
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="encoding">源文本的编码</param>
        /// <returns>返回encoding编码类型中文对应的拼音首字母</returns>
        public static string GetFirstPySpelling (string text, Encoding encoding) {
            if (text.IsNullOrEmpty ())
                return null;
            string temp = ConvertEncoding (text, encoding, Encoding.UTF8);
            return ConvertEncoding (GetFirstPySpelling (temp), Encoding.UTF8, encoding);
        }

        /// <summary>
        /// 取中文文本的拼音
        /// </summary>
        /// <param name="text">编码为UTF8的文本</param>
        /// <param name="connector">拼音之间的连接符</param>
        /// <returns>返回中文文本的拼音</returns>
        public static string GetFullPySpelling (string text, string connector = null) {
            if (connector == null) connector = string.Empty;
            var sbPinyin = new StringBuilder ();
            foreach (char t in text) {
                string py = GetPySpelling (t);
                if (py != string.Empty) sbPinyin.Append (py);
                sbPinyin.Append (connector);
            }

            return sbPinyin.ToString ().TrimEnd (connector.ToCharArray ());
        }

        /// <summary>
        /// 取中文文本的拼音
        /// </summary>
        /// <param name="text">编码为UTF8的文本</param>
        /// <param name="encoding">源文本的编码</param>
        /// <returns>返回encoding编码类型的中文文本的拼音</returns>
        public static string GetFullPySpelling (string text, Encoding encoding) {
            string temp = ConvertEncoding (text.Trim (), encoding, Encoding.UTF8);
            return ConvertEncoding (GetFullPySpelling (temp), Encoding.UTF8, encoding);
        }

        /// <summary>
        /// 返回单个字符的汉字拼音
        /// </summary>
        /// <param name="ch">编码为UTF8的中文字符</param>
        /// <returns>ch对应的拼音</returns>
        public static string GetPySpelling (char ch) {
            short hash = GetHashIndex (ch);
            for (var i = 0; i < PyHash.Hashes[hash].Length; ++i) {
                short index = PyHash.Hashes[hash][i];
                var pos = PyCode.Codes[index].IndexOf (ch, 7);
                if (pos != -1)
                    return PyCode.Codes[index].Substring (0, 6).Trim ();
            }
            return ch.ToString ();
        }

        /// <summary>
        /// 返回单个字符的汉字拼音
        /// </summary>
        /// <param name="ch">编码为encoding的中文字符</param>
        /// <param name="encoding">编码</param>
        /// <returns>编码为encoding的ch对应的拼音</returns>
        public static string GetPySpelling (char ch, Encoding encoding) {
            ch = ConvertEncoding (ch.ToString (), encoding, Encoding.UTF8) [0];
            return ConvertEncoding (GetPySpelling (ch), Encoding.UTF8, encoding);
        }

        /// <summary>
        /// 取和拼音相同的汉字列表
        /// </summary>
        /// <param name="text">UTF8的拼音</param>
        /// <returns>取拼音相同的汉字列表，如拼音“ai”将会返回“唉爱……”等</returns>
        public static string GetChinese (string text) {
            string key = text.Trim ().ToLower ();

            foreach (string str in PyCode.Codes) {
                if (str.StartsWith (key + " ") || str.StartsWith (key + ":"))
                    return str.Substring (7);
            }

            return string.Empty;
        }

        /// <summary>
        /// 取和拼音相同的汉字列表，编码同参数encoding
        /// </summary>
        /// <param name="text">编码为encoding的拼音</param>
        /// <param name="encoding">编码</param>
        /// <returns>返回编码为encoding的拼音为pinyin的汉字列表，如拼音“ai”将会返回“唉爱……”等</returns>
        public static string GetChinese (string text, Encoding encoding) {
            return ConvertEncoding (GetChinese (ConvertEncoding (text, encoding, Encoding.UTF8)), Encoding.UTF8, encoding);
        }

        /// <summary>
        /// 转换编码 
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="srcEncoding">源编码</param>
        /// <param name="dstEncoding">目标编码</param>
        /// <returns>目标编码文本</returns>
        public static string ConvertEncoding (string text, Encoding srcEncoding, Encoding dstEncoding) {
            byte[] srcBytes = srcEncoding.GetBytes (text);
            byte[] dstBytes = Encoding.Convert (srcEncoding, dstEncoding, srcBytes);
            return dstEncoding.GetString (dstBytes);
        }

        /// <summary>
        /// 取文本索引值
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>文本索引值</returns>
        private static short GetHashIndex (char ch) {
            return (short) ((uint) ch % PyCode.Codes.Length);
        }
    }
}