using System;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Security.Encryption;

namespace Aiwins.Rocket.Settings {
    public class SettingEncryptionService : ISettingEncryptionService, ITransientDependency {
        protected IStringEncryptionService StringEncryptionService { get; }

        public SettingEncryptionService (IStringEncryptionService stringEncryptionService) {
            StringEncryptionService = stringEncryptionService;
        }

        public virtual string Encrypt (SettingDefinition settingDefinition, string plainValue) {
            if (plainValue.IsNullOrEmpty ()) {
                return plainValue;
            }

            return StringEncryptionService.Encrypt (plainValue);
        }

        public virtual string Decrypt (SettingDefinition settingDefinition, string encryptedValue) {
            if (encryptedValue.IsNullOrEmpty ()) {
                return encryptedValue;
            }

            return StringEncryptionService.Decrypt (encryptedValue);
        }
    }
}