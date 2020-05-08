using System.Text.RegularExpressions;

namespace Aiwins.Rocket.Validation {
    public class ValidationHelper {
        private const string EmailRegEx = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        
        private const string PhoneRegEx = @"^1[3456789][0-9]{9}$";
        public static bool IsValidEmailAddress (string email) {
            if (string.IsNullOrEmpty (email)) {
                return false;
            }

            /*RFC 2822 (simplified)*/
            return Regex.IsMatch (email, EmailRegEx, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static bool IsValidPhoneNumber (string phone) {
            if (string.IsNullOrEmpty (phone)) {
                return false;
            }

            /*RFC 2822 (simplified)*/
            return Regex.IsMatch (phone, PhoneRegEx, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    }
}