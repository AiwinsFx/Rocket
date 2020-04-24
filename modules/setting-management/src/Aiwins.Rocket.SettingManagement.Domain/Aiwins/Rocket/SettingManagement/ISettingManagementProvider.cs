﻿using System.Threading.Tasks;
using JetBrains.Annotations;
using Aiwins.Rocket.Settings;

namespace Aiwins.Rocket.SettingManagement
{
    public interface ISettingManagementProvider 
    {
        string Name { get; }

        Task<string> GetOrNullAsync([NotNull] SettingDefinition setting, [CanBeNull] string providerKey);

        Task SetAsync([NotNull] SettingDefinition setting, [NotNull] string value, [CanBeNull] string providerKey);

        Task ClearAsync([NotNull] SettingDefinition setting, [CanBeNull] string providerKey);
    }
}
