using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Settings;
using Aiwins.Rocket.Users;

namespace Aiwins.Rocket.SettingManagement {
    public class UserSettingManagementProvider : SettingManagementProvider, ITransientDependency {
        public override string Name => UserSettingValueProvider.ProviderName;

        protected ICurrentUser CurrentUser { get; }

        public UserSettingManagementProvider (
            ISettingManagementStore settingManagementStore,
            ICurrentUser currentUser) : base (settingManagementStore) {
            CurrentUser = currentUser;
        }

        protected override string NormalizeProviderKey (string providerKey) {
            if (providerKey != null) {
                return providerKey;
            }

            return CurrentUser.Id?.ToString ();
        }
    }
}