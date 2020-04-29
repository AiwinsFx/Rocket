using System.Threading.Tasks;

namespace Aiwins.Rocket.FeatureManagement {
    public interface IFeatureManagementStore {
        Task<string> GetOrNullAsync (string name, string providerName, string providerKey);

        Task SetAsync (string name, string value, string providerName, string providerKey);

        Task DeleteAsync (string name, string providerName, string providerKey);
    }
}