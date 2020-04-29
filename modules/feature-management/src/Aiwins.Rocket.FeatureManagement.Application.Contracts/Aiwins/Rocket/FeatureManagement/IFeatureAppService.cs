using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;
using JetBrains.Annotations;

namespace Aiwins.Rocket.FeatureManagement {
    public interface IFeatureAppService : IApplicationService {
        Task<FeatureListDto> GetAsync ([NotNull] string providerName, [NotNull] string providerKey);

        Task UpdateAsync ([NotNull] string providerName, [NotNull] string providerKey, UpdateFeaturesDto input);
    }
}