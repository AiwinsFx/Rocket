using System.Threading.Tasks;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aiwins.Rocket.Account {
    [RemoteService (Name = AccountRemoteServiceConsts.RemoteServiceName)]
    [Area ("account")]
    [Route ("api/account")]
    public class AccountController : RocketController, IAccountAppService {
        protected IAccountAppService AccountAppService { get; }

        public AccountController (IAccountAppService accountAppService) {
            AccountAppService = accountAppService;
        }

        [HttpPost]
        [Route ("register")]
        public virtual Task<IdentityUserDto> RegisterAsync (RegisterDto input) {
            return AccountAppService.RegisterAsync (input);
        }
    }
}