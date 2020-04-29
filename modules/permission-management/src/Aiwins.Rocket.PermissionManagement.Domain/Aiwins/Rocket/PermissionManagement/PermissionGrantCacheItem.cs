using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement {
    [Serializable]
    public class PermissionGrantCacheItem {
        public bool IsGranted { get; set; }
        public string Scope { get; set; }
        public PermissionGrantCacheItem () {

        }

        public PermissionGrantCacheItem (bool isGranted, [NotNull] string scope) {
            IsGranted = isGranted;
            Scope = scope;
        }

        public static string CalculateCacheKey (string name, string providerName, string providerKey) {
            return "pn:" + providerName + ",pk:" + providerKey + ",n:" + name;
        }
    }
}