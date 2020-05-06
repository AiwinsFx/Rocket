using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement {
    public interface IPermissionManagementProvider : ISingletonDependency {
        string Name { get; }

        Task<PermissionGrantInfo> CheckAsync (
            [NotNull] string name, [NotNull] string providerName, [NotNull] string providerKey
        );

        Task SetAsync (
            [NotNull] string name, [NotNull] string providerScope, [NotNull] string providerKey
        );
    }
}