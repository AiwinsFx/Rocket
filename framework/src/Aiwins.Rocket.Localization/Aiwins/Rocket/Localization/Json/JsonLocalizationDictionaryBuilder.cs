using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Aiwins.Rocket.Localization.Json {
    public static class JsonLocalizationDictionaryBuilder {
        private static readonly JsonSerializerSettings SharedJsonSerializerSettings = new JsonSerializerSettings {
            ContractResolver = new CamelCasePropertyNamesContractResolver ()
        };

        /// <summary>
        /// 通过指定文件创建JsonLocalizationDictionaryBuilder  <see cref="JsonLocalizationDictionaryBuilder" /> 。
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        public static ILocalizationDictionary BuildFromFile (string filePath) {
            try {
                return BuildFromJsonString (File.ReadAllText (filePath));
            } catch (Exception ex) {
                throw new RocketException ("Invalid localization file format: " + filePath, ex);
            }
        }

        /// <summary>
        /// 通过指定Json字符串创建JsonLocalizationDictionaryBuilder <see cref="JsonLocalizationDictionaryBuilder" /> 。
        /// </summary>
        /// <param name="jsonString">Json string</param>
        public static ILocalizationDictionary BuildFromJsonString (string jsonString) {
            JsonLocalizationFile jsonFile;
            try {
                jsonFile = JsonConvert.DeserializeObject<JsonLocalizationFile> (
                    jsonString, SharedJsonSerializerSettings);
            } catch (JsonException ex) {
                throw new RocketException ("Can not parse json string. " + ex.Message);
            }

            var cultureCode = jsonFile.Culture;
            if (string.IsNullOrEmpty (cultureCode)) {
                throw new RocketException ("Culture is empty in language json file.");
            }

            var dictionary = new Dictionary<string, LocalizedString> ();
            var dublicateNames = new List<string> ();
            foreach (var item in jsonFile.Texts) {
                if (string.IsNullOrEmpty (item.Key)) {
                    throw new RocketException ("The key is empty in given json string.");
                }

                if (dictionary.GetOrDefault (item.Key) != null) {
                    dublicateNames.Add (item.Key);
                }

                dictionary[item.Key] = new LocalizedString (item.Key, item.Value.NormalizeLineEndings ());
            }

            if (dublicateNames.Count > 0) {
                throw new RocketException (
                    "A dictionary can not contain same key twice. There are some duplicated names: " +
                    dublicateNames.JoinAsString (", "));
            }

            return new StaticLocalizationDictionary (cultureCode, dictionary);
        }
    }
}