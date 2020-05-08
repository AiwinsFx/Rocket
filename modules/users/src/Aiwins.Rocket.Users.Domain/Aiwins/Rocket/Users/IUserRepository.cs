using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.Domain.Repositories;

namespace Aiwins.Rocket.Users {
    public interface IUserRepository<TUser> : IBasicRepository<TUser, Guid>
        where TUser : class, IUser, IAggregateRoot<Guid> {
            Task<TUser> FindByUserNameAsync (string userName, CancellationToken cancellationToken = default);

            Task<List<TUser>> GetListAsync (IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
        }
}