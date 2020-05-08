using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;
using Aiwins.Rocket.Users;

namespace Aiwins.Rocket.Identity {
    public interface IIdentityUserLookupAppService : IApplicationService {
        Task<UserData> FindByIdAsync (Guid id);

        Task<UserData> FindByUserNameAsync (string userName);
    }
}