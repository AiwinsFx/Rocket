using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Users
{
    public interface IExternalUserLookupServiceProvider //TODO: Consider to inherit from IUserLookupService
    {
        Task<IUserData> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IUserData> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    }
}