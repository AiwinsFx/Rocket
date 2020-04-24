using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Aiwins.Rocket.Account.Localization;
using Aiwins.Rocket.Account.Settings;
using Aiwins.Rocket.Account.Web.Areas.Account.Controllers.Models;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Settings;
using Aiwins.Rocket.Validation;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using UserLoginInfo = Aiwins.Rocket.Account.Web.Areas.Account.Controllers.Models.UserLoginInfo;
using IdentityUser = Aiwins.Rocket.Identity.IdentityUser;

namespace Aiwins.Rocket.Account.Web.Areas.Account.Controllers
{
    [RemoteService(Name = AccountRemoteServiceConsts.RemoteServiceName)]
    [Controller]
    [ControllerName("Login")]
    [Area("Account")]
    [Route("api/account")]
    public class AccountController : RocketController
    {
        protected SignInManager<IdentityUser> SignInManager { get; }
        protected IdentityUserManager UserManager { get; }
        protected ISettingProvider SettingProvider { get; }

        public AccountController(SignInManager<IdentityUser> signInManager, IdentityUserManager userManager, ISettingProvider settingProvider)
        {
            LocalizationResource = typeof(AccountResource);

            SignInManager = signInManager;
            UserManager = userManager;
            SettingProvider = settingProvider;
        }

        [HttpPost]
        [Route("login")]
        public virtual async Task<RocketLoginResult> Login(UserLoginInfo login)
        {
            await CheckLocalLoginAsync();

            ValidateLoginInfo(login);

            await ReplaceEmailToUsernameOfInputIfNeeds(login);
            
            return GetRocketLoginResult(await SignInManager.PasswordSignInAsync(
                login.UserNameOrEmailAddress,
                login.Password,
                login.RememberMe,
                true
            ));
        }

        [HttpGet]
        [Route("logout")]
        public virtual async Task Logout()
        {
           await SignInManager.SignOutAsync();
        }

        [HttpPost]
        [Route("checkPassword")]
        public virtual async Task<RocketLoginResult> CheckPassword(UserLoginInfo login)
        {
            ValidateLoginInfo(login);

            await ReplaceEmailToUsernameOfInputIfNeeds(login);

            var identityUser = await UserManager.FindByNameAsync(login.UserNameOrEmailAddress);

            if (identityUser == null)
            {
                return new RocketLoginResult(LoginResultType.InvalidUserNameOrPassword);
            }

            return GetRocketLoginResult(await SignInManager.CheckPasswordSignInAsync(identityUser, login.Password, true));
        }

        protected virtual async Task ReplaceEmailToUsernameOfInputIfNeeds(UserLoginInfo login)
        {
            if (!ValidationHelper.IsValidEmailAddress(login.UserNameOrEmailAddress))
            {
                return;
            }

            var userByUsername = await UserManager.FindByNameAsync(login.UserNameOrEmailAddress);
            if (userByUsername != null)
            {
                return;
            }

            var userByEmail = await UserManager.FindByEmailAsync(login.UserNameOrEmailAddress);
            if (userByEmail == null)
            {
                return;
            }

            login.UserNameOrEmailAddress = userByEmail.UserName;
        }

        private static RocketLoginResult GetRocketLoginResult(SignInResult result)
        {
            if (result.IsLockedOut)
            {
                return new RocketLoginResult(LoginResultType.LockedOut);
            }

            if (result.RequiresTwoFactor)
            {
                return new RocketLoginResult(LoginResultType.RequiresTwoFactor);
            }

            if (result.IsNotAllowed)
            {
                return new RocketLoginResult(LoginResultType.NotAllowed);
            }

            if (!result.Succeeded)
            {
                return new RocketLoginResult(LoginResultType.InvalidUserNameOrPassword);
            }

            return new RocketLoginResult(LoginResultType.Success);
        }

        protected virtual void ValidateLoginInfo(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentException(nameof(login));
            }

            if (login.UserNameOrEmailAddress.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(login.UserNameOrEmailAddress));
            }

            if (login.Password.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(login.Password));
            }
        }

        protected virtual async Task CheckLocalLoginAsync()
        {
            if (!await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin))
            {
                throw new UserFriendlyException(L["LocalLoginDisabledMessage"]);
            }
        }
    }
}
