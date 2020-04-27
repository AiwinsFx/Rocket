namespace Aiwins.Rocket.Pinyin {
    public static class PinyinExtension {
        /// <summary>
        /// 获取汉字拼音大写字母
        /// </summary>
        /// <param name="text">汉字</param>
        /// <returns>汉字拼音大写字母</returns>
        public static string FirstPySpelling (this string text) => Pinyin.GetFirstPySpelling (text);

        /// <summary>
        /// 获取汉字拼音
        /// </summary>
        /// <param name="text">汉字</param>
        /// <returns>汉字拼音</returns>
        public static string FullPySpelling (this string text) => Pinyin.GetFullPySpelling (text).ToUpper ();
    }
}