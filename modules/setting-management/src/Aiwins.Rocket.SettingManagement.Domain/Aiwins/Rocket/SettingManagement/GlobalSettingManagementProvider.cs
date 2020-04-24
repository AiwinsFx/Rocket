using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Settings;

namespace Aiwins.Rocket.SettingManagement
{
    public class GlobalSettingManagementProvider : SettingManagementProvider, ITransientDependency
    {
        public override string Name => GlobalSettingValueProvider.ProviderName;

        public GlobalSettingManagementProvider(ISettingManagementStore settingManagementStore) 
            : base(settingManagementStore)
        {

        }

        protected override string NormalizeProviderKey(string providerKey)
        {
            return null;
        }
    }
}