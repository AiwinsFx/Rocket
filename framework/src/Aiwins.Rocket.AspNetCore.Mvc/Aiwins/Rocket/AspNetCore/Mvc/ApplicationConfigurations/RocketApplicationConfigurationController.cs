using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations
{
    [Area("rocket")]
    [RemoteService(Name = "rocket")]
    [Route("api/rocket/application-configuration")]
    public class RocketApplicationConfigurationController : RocketController, IRocketApplicationConfigurationAppService
    {
        private readonly IRocketApplicationConfigurationAppService _applicationConfigurationAppService;

        public RocketApplicationConfigurationController(
            IRocketApplicationConfigurationAppService applicationConfigurationAppService)
        {
            _applicationConfigurationAppService = applicationConfigurationAppService;
        }

        [HttpGet]
        public async Task<ApplicationConfigurationDto> GetAsync()
        {
            return await _applicationConfigurationAppService.GetAsync();
        }
    }
}