using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Rocket.Application.Services;
using Aiwins.Blogging.Blogs.Dtos;

namespace Aiwins.Blogging.Blogs
{
    public interface IBlogAppService : IApplicationService
    {
        Task<ListResultDto<BlogDto>> GetListAsync();

        Task<BlogDto> GetByShortNameAsync(string shortName);

        Task<BlogDto> GetAsync(Guid id);
        
        Task<BlogDto> Create(CreateBlogDto input);

        Task<BlogDto> Update(Guid id, UpdateBlogDto input);

        Task Delete(Guid id);
    }
}
