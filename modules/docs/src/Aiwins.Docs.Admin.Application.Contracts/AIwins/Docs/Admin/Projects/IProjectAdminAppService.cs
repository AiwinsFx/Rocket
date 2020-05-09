using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Rocket.Application.Services;

namespace Aiwins.Docs.Admin.Projects
{
    public interface IProjectAdminAppService : IApplicationService
    {
        Task<PagedResultDto<ProjectDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        Task<ProjectDto> GetAsync(Guid id);

        Task<ProjectDto> CreateAsync(CreateProjectDto input);

        Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto input);

        Task DeleteAsync(Guid id);

        Task ReindexAsync(ReindexInput input);

        Task ReindexAllAsync();
    }
}
