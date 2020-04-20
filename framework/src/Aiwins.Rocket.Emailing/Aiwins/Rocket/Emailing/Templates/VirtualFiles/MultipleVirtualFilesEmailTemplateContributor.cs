using System;
using System.Collections.Generic;
using Aiwins.Rocket.VirtualFileSystem;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Aiwins.Rocket.Emailing.Templates.VirtualFiles {
    public class MultipleVirtualFilesEmailTemplateContributor : IEmailTemplateContributor {
        private readonly string _virtualPath;

        private IVirtualFileProvider _virtualFileProvider;

        private Dictionary<string, string> _templateDictionary;

        private readonly object _syncObj = new object ();

        public MultipleVirtualFilesEmailTemplateContributor (string virtualPath) {
            _virtualPath = virtualPath;
        }

        public void Initialize (EmailTemplateInitializationContext context) {
            _virtualFileProvider = context.ServiceProvider.GetRequiredService<IVirtualFileProvider> ();
        }

        public string GetOrNull (string cultureName) {
            return GetTemplateDictionary ().GetOrDefault (cultureName);
        }

        private Dictionary<string, string> GetTemplateDictionary () {
            var dictionaries = _templateDictionary;
            if (dictionaries != null) {
                return dictionaries;
            }

            lock (_syncObj) {
                dictionaries = _templateDictionary;
                if (dictionaries != null) {
                    return dictionaries;
                }

                _templateDictionary = new Dictionary<string, string> ();
                foreach (var file in _virtualFileProvider.GetDirectoryContents (_virtualPath)) {
                    if (file.IsDirectory) {
                        continue;
                    }

                    // TODO: 考虑如何规范化文件名称?
                    _templateDictionary.Add (file.Name.RemovePostFix (".tpl"), file.ReadAsString ());
                }

                dictionaries = _templateDictionary;
            }

            return dictionaries;
        }
    }
}