using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Rocket.Application.Services;

namespace Aiwins.Blogging.Posts
{
    public interface IPostAppService : IApplicationService
    {
        Task<ListResultDto<PostWithDetailsDto>> GetListByBlogIdAndTagName(Guid blogId, string tagName);

        Task<ListResultDto<PostWithDetailsDto>> GetTimeOrderedListAsync(Guid blogId);

        Task<PostWithDetailsDto> GetForReadingAsync(GetPostInput input);

        Task<PostWithDetailsDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<PostWithDetailsDto> CreateAsync(CreatePostDto input);

        Task<PostWithDetailsDto> UpdateAsync(Guid id, UpdatePostDto input);
    }
}
