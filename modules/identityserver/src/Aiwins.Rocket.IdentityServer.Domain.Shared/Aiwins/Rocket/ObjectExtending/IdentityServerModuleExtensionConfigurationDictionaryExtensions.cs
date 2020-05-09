using System;
using Aiwins.Rocket.ObjectExtending.Modularity;

namespace Aiwins.Rocket.ObjectExtending {
    public static class IdentityServerModuleExtensionConfigurationDictionaryExtensions {
        public static ModuleExtensionConfigurationDictionary ConfigureIdentityServer (
            this ModuleExtensionConfigurationDictionary modules,
            Action<IdentityServerModuleExtensionConfiguration> configureAction) {
            return modules.ConfigureModule (
                IdentityServerModuleExtensionConsts.ModuleName,
                configureAction
            );
        }
    }
}