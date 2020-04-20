using System.Collections.Generic;

namespace Aiwins.Rocket.Localization.Json {
    public class JsonLocalizationFile {
        /// <summary>
        /// 当前文化; eg : en , en-us, zh-CN
        /// </summary>
        public string Culture { get; set; }

        public Dictionary<string, string> Texts { get; }

        public JsonLocalizationFile () {
            Texts = new Dictionary<string, string> ();
        }
    }
}