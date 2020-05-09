using Aiwins.Rocket.Http.Modeling;
using Microsoft.AspNetCore.Mvc;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApiExploring {
    [Area ("rocket")]
    [RemoteService (Name = "rocket")]
    [Route ("api/rocket/api-definition")]
    public class RocketApiDefinitionController : RocketController, IRemoteService {
        private readonly IApiDescriptionModelProvider _modelProvider;

        public RocketApiDefinitionController (IApiDescriptionModelProvider modelProvider) {
            _modelProvider = modelProvider;
        }

        [HttpGet]
        public ApplicationApiDescriptionModel Get (ApplicationApiDescriptionModelRequestDto model) {
            return _modelProvider.CreateApiModel (model);
        }
    }
}