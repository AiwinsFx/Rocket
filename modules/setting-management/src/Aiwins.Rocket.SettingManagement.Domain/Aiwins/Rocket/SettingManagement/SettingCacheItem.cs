﻿using System;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.SettingManagement
{
    [Serializable]
    [IgnoreMultiTenancy]
    public class SettingCacheItem
    {
        public string Value { get; set; }

        public SettingCacheItem()
        {

        }

        public SettingCacheItem(string value)
        {
            Value = value;
        }

        public static string CalculateCacheKey(string name, string providerName, string providerKey)
        {
            return "pn:" + providerName + ",pk:" + providerKey + ",n:" + name;
        }
    }
}
