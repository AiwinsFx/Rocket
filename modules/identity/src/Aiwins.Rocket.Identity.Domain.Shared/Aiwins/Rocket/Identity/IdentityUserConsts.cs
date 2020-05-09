using Aiwins.Rocket.Users;

namespace Aiwins.Rocket.Identity {
    public static class IdentityUserConsts {
        public const int MaxUserNameLength = RocketUserConsts.MaxUserNameLength;

        public const int MaxNameLength = RocketUserConsts.MaxNameLength;

        public const int MaxSurnameLength = RocketUserConsts.MaxSurnameLength;

        public const int MaxNormalizedUserNameLength = MaxUserNameLength;

        public const int MaxEmailLength = RocketUserConsts.MaxEmailLength;

        public const int MaxNormalizedEmailLength = MaxEmailLength;

        public const int MaxPhoneNumberLength = RocketUserConsts.MaxPhoneNumberLength;

        public const int MaxPasswordLength = 128;

        public const int MaxPasswordHashLength = 256;

        public const int MaxSecurityStampLength = 256;

        public const int MaxConcurrencyStampLength = 256;
    }
}