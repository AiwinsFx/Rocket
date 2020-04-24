using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Rocket.AspNetCore.Mvc;

namespace Aiwins.Rocket.Identity
{
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("Role")]
    [Route("api/identity/roles")]
    public class IdentityRoleController : RocketController, IIdentityRoleAppService
    {
        protected IIdentityRoleAppService RoleAppService { get; }

        public IdentityRoleController(IIdentityRoleAppService roleAppService)
        {
            RoleAppService = roleAppService;
        }

        [HttpGet]
        [Route("all")]
        public virtual Task<ListResultDto<IdentityRoleDto>> GetAllListAsync()
        {
            return RoleAppService.GetAllListAsync();
        }

        [HttpGet]
        public virtual Task<PagedResultDto<IdentityRoleDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return RoleAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<IdentityRoleDto> GetAsync(Guid id)
        {
            return RoleAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
        {
            return RoleAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<IdentityRoleDto> UpdateAsync(Guid id, IdentityRoleUpdateDto input)
        {
            return RoleAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return RoleAppService.DeleteAsync(id);
        }
    }
}
