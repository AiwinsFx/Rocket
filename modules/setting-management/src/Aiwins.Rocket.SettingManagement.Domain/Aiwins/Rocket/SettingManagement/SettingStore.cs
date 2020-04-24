using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Settings;

namespace Aiwins.Rocket.SettingManagement
{
    public class SettingStore : ISettingStore, ITransientDependency
    {
        protected ISettingManagementStore ManagementStore { get; }

        public SettingStore(ISettingManagementStore managementStore)
        {
            ManagementStore = managementStore;
        }

        public virtual Task<string> GetOrNullAsync(string name, string providerName, string providerKey)
        {
            return ManagementStore.GetOrNullAsync(name, providerName, providerKey);
        }
    }
}
