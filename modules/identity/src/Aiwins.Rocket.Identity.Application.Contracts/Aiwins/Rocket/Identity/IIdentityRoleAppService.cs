using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Rocket.Application.Services;

namespace Aiwins.Rocket.Identity {
    public interface IIdentityRoleAppService : IApplicationService {
        Task<ListResultDto<IdentityRoleDto>> GetAllListAsync ();

        Task<PagedResultDto<IdentityRoleDto>> GetListAsync (PagedAndSortedResultRequestDto input);

        Task<IdentityRoleDto> CreateAsync (IdentityRoleCreateDto input);

        Task<IdentityRoleDto> GetAsync (Guid id);

        Task<IdentityRoleDto> UpdateAsync (Guid id, IdentityRoleUpdateDto input);

        Task DeleteAsync (Guid id);
    }
}