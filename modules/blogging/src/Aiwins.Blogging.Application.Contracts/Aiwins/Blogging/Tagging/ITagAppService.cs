using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;
using Aiwins.Blogging.Tagging.Dtos;

namespace Aiwins.Blogging.Tagging
{
    public interface ITagAppService : IApplicationService
    {
        Task<List<TagDto>> GetPopularTags(Guid blogId, GetPopularTagsInput input);

    }
}
