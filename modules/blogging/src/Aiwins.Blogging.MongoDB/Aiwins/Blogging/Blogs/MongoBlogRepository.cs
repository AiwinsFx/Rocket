using System;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using Aiwins.Rocket.Domain.Repositories.MongoDB;
using Aiwins.Rocket.MongoDB;
using Aiwins.Blogging.MongoDB;

namespace Aiwins.Blogging.Blogs
{
    public class MongoBlogRepository : MongoDbRepository<IBloggingMongoDbContext, Blog, Guid>, IBlogRepository
    {
        public MongoBlogRepository(IMongoDbContextProvider<IBloggingMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Blog> FindByShortNameAsync(string shortName)
        {
            return await GetMongoQueryable().FirstOrDefaultAsync(p => p.ShortName == shortName);
        }
    }
}
