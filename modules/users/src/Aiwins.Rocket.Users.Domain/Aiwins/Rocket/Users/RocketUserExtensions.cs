namespace Aiwins.Rocket.Users
{
    public static class RocketUserExtensions
    {
        public static IUserData ToRocketUserData(this IUser user)
        {
            return new UserData(
                id: user.Id,
                userName: user.UserName,
                email: user.Email,
                name: user.Name,
                surname: user.Surname,
                emailConfirmed: user.EmailConfirmed,
                phoneNumber: user.PhoneNumber,
                phoneNumberConfirmed: user.PhoneNumberConfirmed,
                tenantId: user.TenantId
            );
        }
    }
}