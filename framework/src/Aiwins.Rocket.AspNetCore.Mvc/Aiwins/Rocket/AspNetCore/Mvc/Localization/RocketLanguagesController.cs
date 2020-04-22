using System;
using Aiwins.Rocket.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Aiwins.Rocket.AspNetCore.Mvc.Localization {
    [Area ("Rocket")]
    [Route ("Rocket/Languages/[action]")]
    public class RocketLanguagesController : RocketController {
        [HttpGet]
        public IActionResult Switch (string culture, string uiCulture = "", string returnUrl = "") {
            if (!GlobalizationHelper.IsValidCultureCode (culture)) {
                throw new RocketException ("Unknown language: " + culture + ". It must be a valid culture!");
            }

            string cookieValue = CookieRequestCultureProvider.MakeCookieValue (new RequestCulture (culture, uiCulture));

            Response.Cookies.Append (CookieRequestCultureProvider.DefaultCookieName, cookieValue, new CookieOptions {
                Expires = Clock.Now.AddYears (2)
            });

            if (!string.IsNullOrWhiteSpace (returnUrl)) {
                return Redirect (GetRedirectUrl (returnUrl));
            }

            return Redirect ("/");
        }

        private string GetRedirectUrl (string returnUrl) {
            if (returnUrl.IsNullOrEmpty ()) {
                return "/";
            }

            if (Url.IsLocalUrl (returnUrl)) {
                return returnUrl;
            }

            return "/";
        }
    }
}