using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Authorization.Permissions {
    public interface IPermissionStore {
        Task<PermissionGrantResult> GetResultAsync (
            [NotNull] string name, [CanBeNull] string providerName, [CanBeNull] string providerKey
        );
    }
}