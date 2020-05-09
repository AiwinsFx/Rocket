using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Blogging.Tagging;
using Aiwins.Blogging.Tagging.Dtos;

namespace Aiwins.Blogging
{
    [RemoteService(Name = BloggingRemoteServiceConsts.RemoteServiceName)]
    [Area("blogging")]
    [Route("api/blogging/tags")]
    public class TagsController : RocketController, ITagAppService
    {
        private readonly ITagAppService _tagAppService;

        public TagsController(ITagAppService tagAppService)
        {
            _tagAppService = tagAppService;
        }

        [HttpGet]
        [Route("popular/{blogId}")]
        public Task<List<TagDto>> GetPopularTags(Guid blogId, GetPopularTagsInput input)
        {
            return _tagAppService.GetPopularTags(blogId, input);
        }
    }
}
