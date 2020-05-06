using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Text.Formatting;
using Microsoft.Extensions.Localization;

namespace Microsoft.AspNetCore.Identity {
    public static class RocketIdentityResultExtensions {
        public static void CheckErrors (this IdentityResult identityResult) {
            if (identityResult.Succeeded) {
                return;
            }

            if (identityResult.Errors == null) {
                throw new ArgumentException ("identityResult.Errors should not be null.");
            }

            throw new RocketIdentityResultException (identityResult);
        }

        public static string LocalizeErrors (this IdentityResult identityResult, IStringLocalizer localizer) {
            if (identityResult.Succeeded) {
                throw new ArgumentException ("identityResult.Succeeded should be false in order to localize errors.");
            }

            if (identityResult.Errors == null) {
                throw new ArgumentException ("identityResult.Errors should not be null.");
            }

            return identityResult.Errors.Select (err => LocalizeErrorMessage (err, localizer)).JoinAsString (", ");
        }

        public static string LocalizeErrorMessage (this IdentityError error, IStringLocalizer localizer) {
            var key = $"Identity.{error.Code}";

            var localizedString = localizer[key];

            if (!localizedString.ResourceNotFound) {
                var englishLocalizedString = localizer.WithCulture (CultureInfo.GetCultureInfo ("en")) [key];
                if (!englishLocalizedString.ResourceNotFound) {
                    if (FormattedStringValueExtracter.IsMatch (error.Description, englishLocalizedString.Value, out var values)) {
                        return string.Format (localizedString.Value, values.Cast<object> ().ToArray ());
                    }
                }
            }

            return localizer["Identity.Default"];
        }

        public static string GetResultAsString (this SignInResult signInResult) {
            if (signInResult.Succeeded) {
                return "Succeeded";
            }

            if (signInResult.IsLockedOut) {
                return "IsLockedOut";
            }

            if (signInResult.IsNotAllowed) {
                return "IsNotAllowed";
            }

            if (signInResult.RequiresTwoFactor) {
                return "RequiresTwoFactor";
            }

            return "Unknown";
        }
    }
}