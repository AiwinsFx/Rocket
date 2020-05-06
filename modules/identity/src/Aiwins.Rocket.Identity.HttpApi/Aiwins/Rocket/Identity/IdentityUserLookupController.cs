using System;
using System.Threading.Tasks;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.Users;
using Microsoft.AspNetCore.Mvc;

namespace Aiwins.Rocket.Identity {
    [RemoteService (Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area ("identity")]
    [ControllerName ("UserLookup")]
    [Route ("api/identity/users/lookup")]
    public class IdentityUserLookupController : RocketController, IIdentityUserLookupAppService {
        protected IIdentityUserLookupAppService LookupAppService { get; }

        public IdentityUserLookupController (IIdentityUserLookupAppService lookupAppService) {
            LookupAppService = lookupAppService;
        }

        [HttpGet]
        [Route ("{id}")]
        public virtual Task<UserData> FindByIdAsync (Guid id) {
            return LookupAppService.FindByIdAsync (id);
        }

        [HttpGet]
        [Route ("by-username/{userName}")]
        public virtual Task<UserData> FindByUserNameAsync (string userName) {
            return LookupAppService.FindByUserNameAsync (userName);
        }
    }
}