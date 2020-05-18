using Aiwins.Rocket.AspNetCore.Authentication.OAuth.Claims;
using Aiwins.Rocket.Security.Claims;

namespace Microsoft.AspNetCore.Authentication.OAuth.Claims
{
    public static class RocketClaimActionCollectionExtensions
    {
        public static void MapRocketClaimTypes(this ClaimActionCollection claimActions)
        {
            if (RocketClaimTypes.UserName != "preferred_username")
            {
                claimActions.MapJsonKey(RocketClaimTypes.UserName, "preferred_username");
                claimActions.DeleteClaim("preferred_username");
            }

            if (RocketClaimTypes.Name != "name")
            {
                claimActions.MapJsonKey(RocketClaimTypes.Name, "name");
                claimActions.DeleteClaim("name");
            }

            if (RocketClaimTypes.Email != "email")
            {
                claimActions.MapJsonKey(RocketClaimTypes.Email, "email");
                claimActions.DeleteClaim("email");
            }

            if (RocketClaimTypes.EmailVerified != "email_verified")
            {
                claimActions.MapJsonKey(RocketClaimTypes.EmailVerified, "email_verified");
            }

            if (RocketClaimTypes.PhoneNumber != "phone_number")
            {
                claimActions.MapJsonKey(RocketClaimTypes.PhoneNumber, "phone_number");
            }

            if (RocketClaimTypes.PhoneNumberVerified != "phone_number_verified")
            {
                claimActions.MapJsonKey(RocketClaimTypes.PhoneNumberVerified, "phone_number_verified");
            }

            if (RocketClaimTypes.Role != "role")
            {
                claimActions.MapJsonKeyMultiple(RocketClaimTypes.Role, "role");
            }
        }

        public static void MapJsonKeyMultiple(this ClaimActionCollection claimActions, string claimType, string jsonKey)
        {
            claimActions.Add(new MultipleClaimAction(claimType, jsonKey));
        }
    }
}
