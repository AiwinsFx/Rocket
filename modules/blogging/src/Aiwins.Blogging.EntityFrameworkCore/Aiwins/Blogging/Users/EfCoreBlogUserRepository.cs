using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Users.EntityFrameworkCore;
using Aiwins.Blogging.EntityFrameworkCore;

namespace Aiwins.Blogging.Users
{
    public class EfCoreBlogUserRepository : EfCoreUserRepositoryBase<IBloggingDbContext, BlogUser>, IBlogUserRepository
    {
        public EfCoreBlogUserRepository(IDbContextProvider<IBloggingDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<List<BlogUser>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .WhereIf( !string.IsNullOrWhiteSpace( filter), x=>x.UserName.Contains(filter))
                .Take(maxCount).ToListAsync(cancellationToken);
        }
    }
}
