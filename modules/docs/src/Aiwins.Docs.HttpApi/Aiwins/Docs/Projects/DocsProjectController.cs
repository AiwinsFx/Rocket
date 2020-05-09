using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Docs.Documents;

namespace Aiwins.Docs.Projects
{
    [RemoteService]
    [Area("docs")]
    [ControllerName("Project")]
    [Route("api/docs/projects")]
    public class DocsProjectController : RocketController, IProjectAppService
    {
        protected IProjectAppService ProjectAppService { get; }

        public DocsProjectController(IProjectAppService projectAppService)
        {
            ProjectAppService = projectAppService;
        }

        [HttpGet]
        [Route("")]
        public virtual Task<ListResultDto<ProjectDto>> GetListAsync()
        {
            return ProjectAppService.GetListAsync();
        }

        [HttpGet]
        [Route("{shortName}")]
        public virtual Task<ProjectDto> GetAsync(string shortName)
        {
            return ProjectAppService.GetAsync(shortName);
        }

        [HttpGet]
        [Route("{shortName}/defaultLanguage")]
        public Task<string> GetDefaultLanguageCode(string shortName,string version)
        {
            return ProjectAppService.GetDefaultLanguageCode(shortName, version);
        }

        [HttpGet]
        [Route("{shortName}/versions")]
        public virtual Task<ListResultDto<VersionInfoDto>> GetVersionsAsync(string shortName)
        {
            return ProjectAppService.GetVersionsAsync(shortName);
        }

        [HttpGet]
        [Route("{shortName}/{version}/languageList")]
        public Task<LanguageConfig> GetLanguageListAsync(string shortName, string version)
        {
            return ProjectAppService.GetLanguageListAsync(shortName, version);
        }
    }
}
