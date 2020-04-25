using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.DependencyInjection;
using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement {
    public interface IPermissionManagementProvider : ISingletonDependency //TODO: Consider to remove this pre-assumption
    {
        string Name { get; }

        Task<PermissionValueProviderGrantInfo> CheckAsync (
            [NotNull] string name, [NotNull] string providerName, [NotNull] string providerKey
        );

        Task SetAsync (
            [NotNull] string name, [NotNull] string providerKey,
            bool isGranted
        );
    }
}