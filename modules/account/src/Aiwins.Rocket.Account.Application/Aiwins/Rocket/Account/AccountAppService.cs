using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Account.Settings;
using Aiwins.Rocket.Application.Services;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Settings;
using Microsoft.AspNetCore.Identity;

namespace Aiwins.Rocket.Account {
    public class AccountAppService : ApplicationService, IAccountAppService {
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IdentityUserManager UserManager { get; }

        public AccountAppService (
            IdentityUserManager userManager,
            IIdentityRoleRepository roleRepository) {
            RoleRepository = roleRepository;
            UserManager = userManager;
        }

        public virtual async Task<IdentityUserDto> RegisterAsync (RegisterDto input) {
            await CheckSelfRegistrationAsync ();

            var user = new IdentityUser (GuidGenerator.Create (), input.UserName, input.EmailAddress, CurrentTenant.Id);

            (await UserManager.CreateAsync (user, input.Password)).CheckErrors ();

            await UserManager.SetEmailAsync (user, input.EmailAddress);
            await UserManager.AddDefaultRolesAsync (user);

            return ObjectMapper.Map<IdentityUser, IdentityUserDto> (user);
        }

        protected virtual async Task CheckSelfRegistrationAsync () {
            if (!await SettingProvider.IsTrueAsync (AccountSettingNames.IsSelfRegistrationEnabled)) {
                throw new UserFriendlyException (L["SelfRegistrationDisabledMessage"]);
            }
        }
    }
}