using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Blogs.Dtos;

namespace Aiwins.Blogging
{
    [RemoteService(Name = BloggingRemoteServiceConsts.RemoteServiceName)]
    [Area("blogging")]
    [Route("api/blogging/blogs")]
    public class BlogsController : RocketController, IBlogAppService
    {
        private readonly IBlogAppService _blogAppService;

        public BlogsController(IBlogAppService blogAppService)
        {
            _blogAppService = blogAppService;
        }

        [HttpGet]
        public async Task<ListResultDto<BlogDto>> GetListAsync()
        {
            return await _blogAppService.GetListAsync();
        }

        [HttpGet]
        [Route("by-shortname/{shortName}")]
        public async Task<BlogDto> GetByShortNameAsync(string shortName)
        {
            return await _blogAppService.GetByShortNameAsync(shortName);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<BlogDto> GetAsync(Guid id)
        {
            return await _blogAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<BlogDto> Create(CreateBlogDto input)
        {
            return await _blogAppService.Create(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<BlogDto> Update(Guid id, UpdateBlogDto input)
        {
            return await _blogAppService.Update(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id)
        {
            await _blogAppService.Delete(id);
        }
    }
}
