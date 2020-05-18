using System.Security.Claims;

namespace Aiwins.Rocket.Security.Claims {
    /// <summary>
    /// Rocket Claims
    /// </summary>
    public static class RocketClaimTypes {
        /// <summary>
        /// 默认值: <see cref="ClaimTypes.Name"/>
        /// </summary>
        public static string Name { get; set; } = ClaimTypes.Name;

        /// <summary>
        /// 默认值: "preferred_username"
        /// </summary>
        public static string UserName { get; set; } = "preferred_username";

        /// <summary>
        /// 默认值: <see cref="ClaimTypes.NameIdentifier"/>
        /// </summary>
        public static string UserId { get; set; } = ClaimTypes.NameIdentifier;

        /// <summary>
        /// 默认值: <see cref="ClaimTypes.Role"/>
        /// </summary>
        public static string Role { get; set; } = ClaimTypes.Role;

        /// <summary>
        /// 默认值: <see cref="ClaimTypes.Email"/>
        /// </summary>
        public static string Email { get; set; } = ClaimTypes.Email;

        /// <summary>
        /// 默认值: "email_verified".
        /// </summary>
        public static string EmailVerified { get; set; } = "email_verified";

        /// <summary>
        /// 默认值: "phone_number".
        /// </summary>
        public static string PhoneNumber { get; set; } = "phone_number";

        /// <summary>
        /// 默认值: "phone_number_verified".
        /// </summary>
        public static string PhoneNumberVerified { get; set; } = "phone_number_verified";

        /// <summary>
        /// 默认值: "tenant_id".
        /// </summary>
        public static string TenantId { get; set; } = "tenant_id";

        /// <summary>
        /// 默认值: "edition_id".
        /// </summary>
        public static string EditionId { get; set; } = "edition_id";

        /// <summary>
        /// 默认值: "client_id".
        /// </summary>
        public static string ClientId { get; set; } = "client_id";
    }
}