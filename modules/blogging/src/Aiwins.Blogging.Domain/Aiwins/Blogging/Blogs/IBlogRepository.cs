using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories;

namespace Aiwins.Blogging.Blogs
{
    public interface IBlogRepository : IBasicRepository<Blog, Guid>
    {
        Task<Blog> FindByShortNameAsync(string shortName);
    }
}
