using Aiwins.Rocket.Identity.Localization;
using Aiwins.Rocket.Identity.Settings;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Settings;

namespace Aiwins.Rocket.Identity {
    public class RocketIdentitySettingDefinitionProvider : SettingDefinitionProvider {
        public override void Define (ISettingDefinitionContext context) {
            context.Add (
                new SettingDefinition (
                    IdentitySettingNames.Password.RequiredLength,
                    6. ToString (),
                    L ("DisplayName:Rocket.Identity.Password.RequiredLength"),
                    L ("Description:Rocket.Identity.Password.RequiredLength"),
                    true),

                new SettingDefinition (
                    IdentitySettingNames.Password.RequiredUniqueChars,
                    1. ToString (),
                    L ("DisplayName:Rocket.Identity.Password.RequiredUniqueChars"),
                    L ("Description:Rocket.Identity.Password.RequiredUniqueChars"),
                    true),

                new SettingDefinition (
                    IdentitySettingNames.Password.RequireNonAlphanumeric,
                    true.ToString (),
                    L ("DisplayName:Rocket.Identity.Password.RequireNonAlphanumeric"),
                    L ("Description:Rocket.Identity.Password.RequireNonAlphanumeric"),
                    true),

                new SettingDefinition (
                    IdentitySettingNames.Password.RequireLowercase,
                    true.ToString (), L ("DisplayName:Rocket.Identity.Password.RequireLowercase"),
                    L ("Description:Rocket.Identity.Password.RequireLowercase"),
                    true),

                new SettingDefinition (
                    IdentitySettingNames.Password.RequireUppercase,
                    true.ToString (), L ("DisplayName:Rocket.Identity.Password.RequireUppercase"),
                    L ("Description:Rocket.Identity.Password.RequireUppercase"),
                    true),

                new SettingDefinition (
                    IdentitySettingNames.Password.RequireDigit,
                    true.ToString (), L ("DisplayName:Rocket.Identity.Password.RequireDigit"),
                    L ("Description:Rocket.Identity.Password.RequireDigit"),
                    true),

                new SettingDefinition (
                    IdentitySettingNames.Lockout.AllowedForNewUsers,
                    true.ToString (), L ("DisplayName:Rocket.Identity.Lockout.AllowedForNewUsers"),
                    L ("Description:Rocket.Identity.Lockout.AllowedForNewUsers"),
                    true),

                new SettingDefinition (
                    IdentitySettingNames.Lockout.LockoutDuration,
                    (5 * 60).ToString (), L ("DisplayName:Rocket.Identity.Lockout.LockoutDuration"),
                    L ("Description:Rocket.Identity.Lockout.LockoutDuration"),
                    true),

                new SettingDefinition (
                    IdentitySettingNames.Lockout.MaxFailedAccessAttempts,
                    5. ToString (), L ("DisplayName:Rocket.Identity.Lockout.MaxFailedAccessAttempts"),
                    L ("Description:Rocket.Identity.Lockout.MaxFailedAccessAttempts"),
                    true),

                new SettingDefinition (
                    IdentitySettingNames.SignIn.RequireConfirmedEmail,
                    false.ToString (), L ("DisplayName:Rocket.Identity.SignIn.RequireConfirmedEmail"),
                    L ("Description:Rocket.Identity.SignIn.RequireConfirmedEmail"),
                    true),
                new SettingDefinition (
                    IdentitySettingNames.SignIn.EnablePhoneNumberConfirmation,
                    true.ToString (), L ("DisplayName:Rocket.Identity.SignIn.EnablePhoneNumberConfirmation"),
                    L ("Description:Rocket.Identity.SignIn.EnablePhoneNumberConfirmation"),
                    true),
                new SettingDefinition (
                    IdentitySettingNames.SignIn.RequireConfirmedPhoneNumber,
                    false.ToString (), L ("DisplayName:Rocket.Identity.SignIn.RequireConfirmedPhoneNumber"),
                    L ("Description:Rocket.Identity.SignIn.RequireConfirmedPhoneNumber"),
                    true),

                new SettingDefinition (
                    IdentitySettingNames.User.IsUserNameUpdateEnabled,
                    true.ToString (), L ("DisplayName:Rocket.Identity.User.IsUserNameUpdateEnabled"),
                    L ("Description:Rocket.Identity.User.IsUserNameUpdateEnabled"),
                    true),

                new SettingDefinition (
                    IdentitySettingNames.User.IsEmailUpdateEnabled,
                    true.ToString (), L ("DisplayName:Rocket.Identity.User.IsEmailUpdateEnabled"),
                    L ("Description:Rocket.Identity.User.IsEmailUpdateEnabled"),
                    true)
            );
        }

        private static LocalizableString L (string name) {
            return LocalizableString.Create<IdentityResource> (name);
        }
    }
}