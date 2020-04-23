using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.AspNetCore.Mvc.MultiTenancy;

namespace Pages.Rocket.MultiTenancy
{
    [Route("api/rocket/multi-tenancy")]
    public class RocketTenantController : RocketController, IRocketTenantAppService
    {
        private readonly IRocketTenantAppService _rocketTenantAppService;

        public RocketTenantController(IRocketTenantAppService rocketTenantAppService)
        {
            _rocketTenantAppService = rocketTenantAppService;
        }

        [HttpGet]
        [Route("tenants/by-name/{name}")]
        public async Task<FindTenantResultDto> FindTenantByNameAsync(string name)
        {
            return await _rocketTenantAppService.FindTenantByNameAsync(name);
        }

        [HttpGet]
        [Route("tenants/by-id/{id}")]
        public async Task<FindTenantResultDto> FindTenantByIdAsync(Guid id)
        {
            return await _rocketTenantAppService.FindTenantByIdAsync(id);
        }
    }
}
