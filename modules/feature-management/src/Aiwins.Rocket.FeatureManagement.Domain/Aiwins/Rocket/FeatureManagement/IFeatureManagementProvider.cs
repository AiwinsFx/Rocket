using System.Threading.Tasks;
using Aiwins.Rocket.Features;
using JetBrains.Annotations;

namespace Aiwins.Rocket.FeatureManagement {
    public interface IFeatureManagementProvider {
        string Name { get; }

        Task<string> GetOrNullAsync ([NotNull] FeatureDefinition feature, [CanBeNull] string providerKey);

        Task SetAsync ([NotNull] FeatureDefinition feature, [NotNull] string value, [CanBeNull] string providerKey);

        Task ClearAsync ([NotNull] FeatureDefinition feature, [CanBeNull] string providerKey);
    }
}