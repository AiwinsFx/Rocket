using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Settings {
    public interface ISettingStore {
        Task<string> GetOrNullAsync (
            [NotNull] string name, [CanBeNull] string providerName, [CanBeNull] string providerKey
        );
    }
}