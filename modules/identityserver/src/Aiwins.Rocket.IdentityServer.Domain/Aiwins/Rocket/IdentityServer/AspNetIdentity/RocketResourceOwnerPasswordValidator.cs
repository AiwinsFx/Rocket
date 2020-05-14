using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aiwins.Rocket.IdentityServer.Localization;
using Aiwins.Rocket.Security.Claims;
using Aiwins.Rocket.Uow;
using Aiwins.Rocket.Validation;
using IdentityModel;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using IdentityUser = Aiwins.Rocket.Identity.IdentityUser;

namespace Aiwins.Rocket.IdentityServer.AspNetIdentity {
    public class RocketResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator {
        protected SignInManager<IdentityUser> SignInManager { get; }
        protected IEventService Events { get; }
        protected UserManager<IdentityUser> UserManager { get; }
        protected ILogger<ResourceOwnerPasswordValidator<IdentityUser>> Logger { get; }
        protected IStringLocalizer<RocketIdentityServerResource> Localizer { get; }

        public RocketResourceOwnerPasswordValidator (
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEventService events,
            ILogger<ResourceOwnerPasswordValidator<IdentityUser>> logger,
            IStringLocalizer<RocketIdentityServerResource> localizer) {
            UserManager = userManager;
            SignInManager = signInManager;
            Events = events;
            Logger = logger;
            Localizer = localizer;
        }

        /// <summary>
        /// https://github.com/IdentityServer/IdentityServer4/blob/master/src/AspNetIdentity/src/ResourceOwnerPasswordValidator.cs#L53
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task ValidateAsync (ResourceOwnerPasswordValidationContext context) {
            // 默认手机号登录、支持邮箱登录
            await ReplaceEmailToUsernameOfInputIfNeeds (context);
            var user = await UserManager.FindByNameAsync (context.UserName);
            string errorDescription;
            if (user != null) {
                var result = await SignInManager.CheckPasswordSignInAsync (user, context.Password, true);
                if (result.Succeeded) {
                    var sub = await UserManager.GetUserIdAsync (user);

                    Logger.LogInformation ("Credentials validated for username: {username}", context.UserName);
                    await Events.RaiseAsync (new UserLoginSuccessEvent (context.UserName, sub, context.UserName, interactive : false));

                    var additionalClaims = new List<Claim> ();

                    await AddCustomClaimsAsync (additionalClaims, user, context);

                    context.Result = new GrantValidationResult (
                        sub,
                        OidcConstants.AuthenticationMethods.Password,
                        additionalClaims.ToArray ()
                    );

                    return;
                } else if (result.IsLockedOut) {
                    Logger.LogInformation ("Authentication failed for username: {username}, reason: locked out", context.UserName);
                    await Events.RaiseAsync (new UserLoginFailureEvent (context.UserName, "locked out", interactive : false));
                    errorDescription = Localizer["UserLockedOut"];
                } else if (result.IsNotAllowed) {
                    Logger.LogInformation ("Authentication failed for username: {username}, reason: not allowed", context.UserName);
                    await Events.RaiseAsync (new UserLoginFailureEvent (context.UserName, "not allowed", interactive : false));
                    errorDescription = Localizer["LoginIsNotAllowed"];
                } else {
                    Logger.LogInformation ("Authentication failed for username: {username}, reason: invalid credentials", context.UserName);
                    await Events.RaiseAsync (new UserLoginFailureEvent (context.UserName, "invalid credentials", interactive : false));
                    errorDescription = Localizer["InvalidUserNameOrPassword"];
                }
            } else {
                Logger.LogInformation ("No user found matching username: {username}", context.UserName);
                await Events.RaiseAsync (new UserLoginFailureEvent (context.UserName, "invalid username", interactive : false));
                errorDescription = Localizer["InvalidUsername"];
            }

            context.Result = new GrantValidationResult (TokenRequestErrors.InvalidGrant, errorDescription);
        }

        protected virtual async Task ReplacePhoneNumberToUsernameOfInputIfNeeds (ResourceOwnerPasswordValidationContext context) {
            if (!ValidationHelper.IsValidPhoneNumber (context.UserName)) {
                return;
            }

            var userByUsername = await UserManager.FindByNameAsync (context.UserName);
            if (userByUsername != null) {
                return;
            }

            // TODO：此处同步方法获取用户信息，可以考虑优化
            var userByPhoneNumber = UserManager.Users.FirstOrDefault(m => m.PhoneNumber == context.UserName);
            if (userByPhoneNumber == null) {
                return;
            }

            context.UserName = userByPhoneNumber.UserName;
        }

        protected virtual async Task ReplaceEmailToUsernameOfInputIfNeeds (ResourceOwnerPasswordValidationContext context) {
            if (!ValidationHelper.IsValidEmailAddress (context.UserName)) {
                return;
            }

            var userByUsername = await UserManager.FindByNameAsync (context.UserName);
            if (userByUsername != null) {
                return;
            }

            var userByEmail = await UserManager.FindByEmailAsync (context.UserName);
            if (userByEmail == null) {
                return;
            }

            context.UserName = userByEmail.UserName;
        }

        protected virtual Task AddCustomClaimsAsync (List<Claim> customClaims, IdentityUser user, ResourceOwnerPasswordValidationContext context) {
            if (user.TenantId.HasValue) {
                customClaims.Add (new Claim (RocketClaimTypes.TenantId, user.TenantId?.ToString ()));
            }

            return Task.CompletedTask;
        }
    }
}