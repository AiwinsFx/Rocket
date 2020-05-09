using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Blogging.EntityFrameworkCore;

namespace Aiwins.Blogging.Blogs
{
    public class EfCoreBlogRepository : EfCoreRepository<IBloggingDbContext, Blog, Guid>, IBlogRepository
    {
        public EfCoreBlogRepository(IDbContextProvider<IBloggingDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<Blog> FindByShortNameAsync(string shortName)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.ShortName == shortName);
        }
    }
}
