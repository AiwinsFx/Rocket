﻿using System.Collections.Generic;

namespace Aiwins.Rocket.Ldap {
    public static class LdapHelps {
        public static string BuildCondition (string name, string value) {
            return string.IsNullOrWhiteSpace (value) ? "" : $"({name}={value})";
        }

        public static string BuildFilter (Dictionary<string, string> conditions) {
            if (null == conditions) {
                conditions = new Dictionary<string, string> ();
            }

            if (conditions.Keys.Count == 0) {
                conditions.Add ("objectClass", "*"); // 添加默认条件
            }

            var subFilter = string.Empty;
            foreach (var keyValuePair in conditions) {
                subFilter += BuildCondition (keyValuePair.Key, keyValuePair.Value);
            }

            return $"(&{subFilter})";
        }

    }
}