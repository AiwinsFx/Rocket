using System.Threading.Tasks;
using Aiwins.Rocket.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Aiwins.Rocket.FeatureManagement {
    [RemoteService (Name = FeatureManagementRemoteServiceConsts.RemoteServiceName)]
    [Area ("rocket")]
    public class FeaturesController : RocketController, IFeatureAppService {
        protected IFeatureAppService FeatureAppService { get; }

        public FeaturesController (IFeatureAppService featureAppService) {
            FeatureAppService = featureAppService;
        }

        public virtual Task<FeatureListDto> GetAsync (string providerName, string providerKey) {
            return FeatureAppService.GetAsync (providerName, providerKey);
        }

        public virtual Task UpdateAsync (string providerName, string providerKey, UpdateFeaturesDto input) {
            return FeatureAppService.UpdateAsync (providerName, providerKey, input);
        }
    }
}