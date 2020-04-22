using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;

namespace Aiwins.Rocket.AspNetCore.Mvc.MultiTenancy {
    public interface IRocketTenantAppService : IApplicationService {
        Task<FindTenantResultDto> FindTenantByNameAsync (string name);

        Task<FindTenantResultDto> FindTenantByIdAsync (Guid id);
    }
}