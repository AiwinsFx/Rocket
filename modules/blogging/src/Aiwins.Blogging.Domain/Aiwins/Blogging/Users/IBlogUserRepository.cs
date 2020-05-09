using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories;
using Aiwins.Rocket.Users;

namespace Aiwins.Blogging.Users
{
    public interface IBlogUserRepository : IBasicRepository<BlogUser, Guid>, IUserRepository<BlogUser>
    {
        Task<List<BlogUser>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default);
    }
}