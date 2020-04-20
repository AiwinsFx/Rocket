using System.Threading.Tasks;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.Settings {
    public class TenantSettingValueProvider : SettingValueProvider {
        public const string ProviderName = "T";

        public override string Name => ProviderName;

        protected ICurrentTenant CurrentTenant { get; }

        public TenantSettingValueProvider (ISettingStore settingStore, ICurrentTenant currentTenant) : base (settingStore) {
            CurrentTenant = currentTenant;
        }

        public override async Task<string> GetOrNullAsync (SettingDefinition setting) {
            return await SettingStore.GetOrNullAsync (setting.Name, Name, CurrentTenant.Id?.ToString ());
        }
    }
}