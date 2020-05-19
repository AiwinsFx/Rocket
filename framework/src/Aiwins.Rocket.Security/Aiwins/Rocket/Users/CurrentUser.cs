using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Security.Claims;

namespace Aiwins.Rocket.Users {
    public class CurrentUser : ICurrentUser, ITransientDependency {
        private static readonly Claim[] EmptyClaimsArray = new Claim[0];

        public virtual bool IsAuthenticated => Id.HasValue;

        public virtual Guid? Id => _principalAccessor.Principal?.FindUserId ();

        public virtual string Name => this.FindClaimValue (RocketClaimTypes.Name);

        public virtual string UserName => this.FindClaimValue (RocketClaimTypes.UserName);

        public virtual string PhoneNumber => this.FindClaimValue (RocketClaimTypes.PhoneNumber);

        public virtual bool PhoneNumberVerified => string.Equals (this.FindClaimValue (RocketClaimTypes.PhoneNumberVerified), "true", StringComparison.InvariantCultureIgnoreCase);

        public virtual string Email => this.FindClaimValue (RocketClaimTypes.Email);

        public virtual bool EmailVerified => string.Equals (this.FindClaimValue (RocketClaimTypes.EmailVerified), "true", StringComparison.InvariantCultureIgnoreCase);

        public virtual Guid? TenantId => _principalAccessor.Principal?.FindTenantId ();

        public virtual string[] Roles => FindClaims (RocketClaimTypes.Role).Select (c => c.Value).ToArray ();

        public virtual Guid[] RoleIds => FindClaims (RocketClaimTypes.RoleId).Select (c => new Guid(c.Value)).ToArray ();

        private readonly ICurrentPrincipalAccessor _principalAccessor;

        public CurrentUser (ICurrentPrincipalAccessor principalAccessor) {
            _principalAccessor = principalAccessor;
        }

        public virtual Claim FindClaim (string claimType) {
            return _principalAccessor.Principal?.Claims.FirstOrDefault (c => c.Type == claimType);
        }

        public virtual Claim[] FindClaims (string claimType) {
            return _principalAccessor.Principal?.Claims.Where (c => c.Type == claimType).ToArray () ?? EmptyClaimsArray;
        }

        public virtual Claim[] GetAllClaims () {
            return _principalAccessor.Principal?.Claims.ToArray () ?? EmptyClaimsArray;
        }

        public virtual bool IsInRole (string roleName) {
            return FindClaims (RocketClaimTypes.Role).Any (c => c.Value == roleName);
        }

        public virtual bool IsInRole (Guid roleId) {
            return FindClaims (RocketClaimTypes.RoleId).Any (c => c.Value == roleId.ToString ());
        }
    }
}