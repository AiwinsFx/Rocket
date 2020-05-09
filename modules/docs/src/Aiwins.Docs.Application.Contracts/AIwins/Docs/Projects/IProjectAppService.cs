using System.Threading.Tasks;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Rocket.Application.Services;
using Aiwins.Docs.Documents;

namespace Aiwins.Docs.Projects
{
    public interface IProjectAppService : IApplicationService
    {
        Task<ListResultDto<ProjectDto>> GetListAsync();
     
        Task<ProjectDto> GetAsync(string shortName);
        
        Task<ListResultDto<VersionInfoDto>> GetVersionsAsync(string shortName);

        Task<string> GetDefaultLanguageCode(string shortName, string version);

        Task<LanguageConfig> GetLanguageListAsync(string shortName, string version);
    }
}