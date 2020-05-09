using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.MongoDB;
using Aiwins.Rocket.Users.MongoDB;
using Aiwins.Blogging.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Aiwins.Blogging.Users
{
    public class MongoBlogUserRepository : MongoUserRepositoryBase<IBloggingMongoDbContext, BlogUser>, IBlogUserRepository
    {
        public MongoBlogUserRepository(IMongoDbContextProvider<IBloggingMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<BlogUser>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken)
        {
            var query = GetMongoQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(x => x.UserName.Contains(filter));
            }

            return await query.Take(maxCount).ToListAsync(cancellationToken);
        }
    }
}
