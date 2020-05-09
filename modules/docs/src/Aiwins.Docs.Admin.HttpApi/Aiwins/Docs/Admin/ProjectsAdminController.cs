﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Docs.Admin.Projects;

namespace Aiwins.Docs.Admin
{
    [RemoteService]
    [Area("docs")]
    [ControllerName("ProjectsAdmin")]
    [Route("api/docs/admin/projects")]
    public class ProjectsAdminController : RocketController, IProjectAdminAppService
    {
        private readonly IProjectAdminAppService _projectAppService;

        public ProjectsAdminController(IProjectAdminAppService projectAdminAppService)
        {
            _projectAppService = projectAdminAppService;
        }

        [HttpGet]
        [Route("")]
        public Task<PagedResultDto<ProjectDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _projectAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ProjectDto> GetAsync(Guid id)
        {
            return _projectAppService.GetAsync(id);
        }

        [HttpPost]
        public Task<ProjectDto> CreateAsync(CreateProjectDto input)
        {
            return _projectAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto input)
        {
            return _projectAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        public Task DeleteAsync(Guid id)
        {
            return _projectAppService.DeleteAsync(id);
        }

        [HttpPost]
        [Route("ReindexAll")]
        public Task ReindexAllAsync()
        {
            return _projectAppService.ReindexAllAsync();
        }

        [HttpPost]
        [Route("Reindex")]
        public Task ReindexAsync(ReindexInput input)
        {
            return _projectAppService.ReindexAsync(input);
        }
    }
}