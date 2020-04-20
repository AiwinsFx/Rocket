using System;
using Aiwins.Rocket.Localization.Json;
using Microsoft.Extensions.FileProviders;

namespace Aiwins.Rocket.Localization.VirtualFiles.Json {
    //TODO: 考虑使用接口组合而非继承实现

    public class JsonVirtualFileLocalizationResourceContributor : VirtualFileLocalizationResourceContributorBase {
        public JsonVirtualFileLocalizationResourceContributor (string virtualPath) : base (virtualPath) {

        }

        protected override bool CanParseFile (IFileInfo file) {
            return file.Name.EndsWith (".json", StringComparison.OrdinalIgnoreCase);
        }

        protected override ILocalizationDictionary CreateDictionaryFromFileContent (string jsonString) {
            return JsonLocalizationDictionaryBuilder.BuildFromJsonString (jsonString);
        }
    }
}