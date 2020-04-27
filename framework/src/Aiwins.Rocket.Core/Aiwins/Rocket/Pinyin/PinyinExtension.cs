namespace Penguin.Core.Utility.Pinyin {
    public static class PinyinExtension {
        /// <summary>
        /// 获取汉字拼音首字母
        /// </summary>
        /// <param name="text">汉字</param>
        /// <returns>汉字拼音首字母</returns>
        public static char GetPyInitial (this string text) => Pinyin.GetInitials (text) [0];

        /// <summary>
        /// 获取汉字拼音大写字母
        /// </summary>
        /// <param name="text">汉字</param>
        /// <returns>汉字拼音大写字母</returns>
        public static string GetPyInitials (this string text) => Pinyin.GetInitials (text);

        /// <summary>
        /// 获取汉字拼音
        /// </summary>
        /// <param name="text">汉字</param>
        /// <returns>汉字拼音</returns>
        public static string GetPinyin (this string text) => Pinyin.GetPinyin (text).ToUpper ();
    }
}