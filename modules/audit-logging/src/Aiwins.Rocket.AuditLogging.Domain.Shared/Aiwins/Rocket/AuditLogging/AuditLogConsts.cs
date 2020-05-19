﻿using Aiwins.Rocket.Users;
namespace Aiwins.Rocket.AuditLogging {
    public static class AuditLogConsts {
        public const int MaxApplicationNameLength = 96;

        public const int MaxClientIpAddressLength = 64;

        public const int MaxClientNameLength = 128;

        public const int MaxClientIdLength = 64;

        public const int MaxCorrelationIdLength = 64;

        public const int MaxBrowserInfoLength = 512;

        public const int MaxExceptionsLength = 4000;

        public const int MaxCommentsLength = 256;

        public const int MaxUrlLength = 256;

        public const int MaxHttpMethodLength = 16;
        
        public const int MaxTenantNameLength = 64;

        public const int MaxUserNameLength = RocketUserConsts.MaxNameLength;

        public const int MaxUserPhoneNumberLength = RocketUserConsts.MaxPhoneNumberLength;
    }
}