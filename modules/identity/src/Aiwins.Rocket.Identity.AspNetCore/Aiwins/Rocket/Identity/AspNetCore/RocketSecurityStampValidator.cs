using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.Uow;

namespace Aiwins.Rocket.Identity.AspNetCore
{
    public class RocketSecurityStampValidator : SecurityStampValidator<IdentityUser>
    {
        public RocketSecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options,
            SignInManager<IdentityUser> signInManager,
            ISystemClock systemClock,
            ILoggerFactory loggerFactory)
            : base(
                options, 
                signInManager,
                systemClock,
                loggerFactory)
        {
        }

        [UnitOfWork]
        public override Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            return base.ValidateAsync(context);
        }
    }
}
