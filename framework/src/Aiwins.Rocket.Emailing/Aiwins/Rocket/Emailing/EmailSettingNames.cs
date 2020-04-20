namespace Aiwins.Rocket.Emailing {
    /// <summary>
    /// 配置字段 <see cref="EmailSettingProvider"/>.
    /// </summary>
    public static class EmailSettingNames {
        /// <summary>
        /// Rocket.Net.Mail.DefaultFromAddress
        /// </summary>
        public const string DefaultFromAddress = "Rocket.Mailing.DefaultFromAddress";

        /// <summary>
        /// Rocket.Net.Mail.DefaultFromDisplayName
        /// </summary>
        public const string DefaultFromDisplayName = "Rocket.Mailing.DefaultFromDisplayName";

        /// <summary>
        /// SMTP 配置字段
        /// </summary>
        public static class Smtp {
            /// <summary>
            /// Rocket.Net.Mail.Smtp.Host
            /// </summary>
            public const string Host = "Rocket.Mailing.Smtp.Host";

            /// <summary>
            /// Rocket.Net.Mail.Smtp.Port
            /// </summary>
            public const string Port = "Rocket.Mailing.Smtp.Port";

            /// <summary>
            /// Rocket.Net.Mail.Smtp.UserName
            /// </summary>
            public const string UserName = "Rocket.Mailing.Smtp.UserName";

            /// <summary>
            /// Rocket.Net.Mail.Smtp.Password
            /// </summary>
            public const string Password = "Rocket.Mailing.Smtp.Password";

            /// <summary>
            /// Rocket.Net.Mail.Smtp.Domain
            /// </summary>
            public const string Domain = "Rocket.Mailing.Smtp.Domain";

            /// <summary>
            /// Rocket.Net.Mail.Smtp.EnableSsl
            /// </summary>
            public const string EnableSsl = "Rocket.Mailing.Smtp.EnableSsl";

            /// <summary>
            /// Rocket.Net.Mail.Smtp.UseDefaultCredentials
            /// </summary>
            public const string UseDefaultCredentials = "Rocket.Mailing.Smtp.UseDefaultCredentials";
        }
    }
}