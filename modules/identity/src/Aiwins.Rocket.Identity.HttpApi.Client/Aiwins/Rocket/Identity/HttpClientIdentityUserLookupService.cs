using System;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Users;

namespace Aiwins.Rocket.Identity
{
    [Dependency(TryRegister = true)]
    public class HttpClientExternalUserLookupServiceProvider : IExternalUserLookupServiceProvider, ITransientDependency
    {
        protected IIdentityUserLookupAppService UserLookupAppService { get; }

        public HttpClientExternalUserLookupServiceProvider(IIdentityUserLookupAppService userLookupAppService)
        {
            UserLookupAppService = userLookupAppService;
        }

        public virtual async Task<IUserData> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await UserLookupAppService.FindByIdAsync(id);
        }

        public virtual async Task<IUserData> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await UserLookupAppService.FindByUserNameAsync(userName);
        }
    }
}
