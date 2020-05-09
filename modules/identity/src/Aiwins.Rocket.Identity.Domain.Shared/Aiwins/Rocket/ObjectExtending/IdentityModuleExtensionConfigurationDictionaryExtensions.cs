using System;
using Aiwins.Rocket.ObjectExtending.Modularity;

namespace Aiwins.Rocket.ObjectExtending {
    public static class IdentityModuleExtensionConfigurationDictionaryExtensions {
        public static ModuleExtensionConfigurationDictionary ConfigureIdentity (
            this ModuleExtensionConfigurationDictionary modules,
            Action<IdentityModuleExtensionConfiguration> configureAction) {
            return modules.ConfigureModule (
                IdentityModuleExtensionConsts.ModuleName,
                configureAction
            );
        }
    }
}