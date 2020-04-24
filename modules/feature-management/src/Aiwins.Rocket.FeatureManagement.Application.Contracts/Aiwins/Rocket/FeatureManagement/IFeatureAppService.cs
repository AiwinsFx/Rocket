using System.Threading.Tasks;
using JetBrains.Annotations;
using Aiwins.Rocket.Application.Services;

namespace Aiwins.Rocket.FeatureManagement
{
    public interface IFeatureAppService : IApplicationService
    {
        Task<FeatureListDto> GetAsync([NotNull] string providerName, [NotNull] string providerKey); 

        Task UpdateAsync([NotNull] string providerName, [NotNull] string providerKey, UpdateFeaturesDto input); 
    }
}
