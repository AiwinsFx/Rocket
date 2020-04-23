using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;
using Aiwins.Rocket.AspNetCore.Mvc.MultiTenancy;
using Aiwins.Rocket.MultiTenancy;

namespace Pages.Rocket.MultiTenancy
{
    public class RocketTenantAppService : ApplicationService, IRocketTenantAppService
    {
        protected ITenantStore TenantStore { get; }

        public RocketTenantAppService(ITenantStore tenantStore)
        {
            TenantStore = tenantStore;
        }

        public async Task<FindTenantResultDto> FindTenantByNameAsync(string name)
        {
            var tenant = await TenantStore.FindAsync(name);

            if (tenant == null)
            {
                return new FindTenantResultDto { Success = false };
            }

            return new FindTenantResultDto
            {
                Success = true,
                TenantId = tenant.Id,
                Name = tenant.Name
            };
        }
        
        public async Task<FindTenantResultDto> FindTenantByIdAsync(Guid id)
        {
            var tenant = await TenantStore.FindAsync(id);

            if (tenant == null)
            {
                return new FindTenantResultDto { Success = false };
            }

            return new FindTenantResultDto
            {
                Success = true,
                TenantId = tenant.Id,
                Name = tenant.Name
            };
        }
    }
}