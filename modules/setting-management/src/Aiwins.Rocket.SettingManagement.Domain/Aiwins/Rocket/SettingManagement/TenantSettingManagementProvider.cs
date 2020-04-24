using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.Settings;

namespace Aiwins.Rocket.SettingManagement
{
    public class TenantSettingManagementProvider : SettingManagementProvider, ITransientDependency
    {
        public override string Name => TenantSettingValueProvider.ProviderName;

        protected ICurrentTenant CurrentTenant { get; }

        public TenantSettingManagementProvider(
            ISettingManagementStore settingManagementStore,
            ICurrentTenant currentTenant)
            : base(settingManagementStore)
        {
            CurrentTenant = currentTenant;
        }

        protected override string NormalizeProviderKey(string providerKey)
        {
            if (providerKey != null)
            {
                return providerKey;
            }

            return CurrentTenant.Id?.ToString();
        }
    }
}