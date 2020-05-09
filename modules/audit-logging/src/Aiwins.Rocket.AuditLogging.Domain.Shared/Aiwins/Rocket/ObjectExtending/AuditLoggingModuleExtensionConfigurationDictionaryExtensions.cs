using System;
using Aiwins.Rocket.ObjectExtending.Modularity;

namespace Aiwins.Rocket.ObjectExtending {
    public static class AuditLoggingModuleExtensionConfigurationDictionaryExtensions {
        public static ModuleExtensionConfigurationDictionary ConfigureAuditLogging (
            this ModuleExtensionConfigurationDictionary modules,
            Action<AuditLoggingModuleExtensionConfiguration> configureAction) {
            return modules.ConfigureModule (
                AuditLoggingModuleExtensionConsts.ModuleName,
                configureAction
            );
        }
    }
}