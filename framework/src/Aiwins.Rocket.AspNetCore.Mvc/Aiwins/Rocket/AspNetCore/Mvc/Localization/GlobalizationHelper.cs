﻿using System;
using System.Globalization;

namespace Aiwins.Rocket.AspNetCore.Mvc.Localization {
    internal static class GlobalizationHelper {
        public static bool IsValidCultureCode (string cultureCode) {
            if (cultureCode.IsNullOrWhiteSpace ()) {
                return false;
            }

            try {
                CultureInfo.GetCultureInfo (cultureCode);
                return true;
            } catch (CultureNotFoundException) {
                return false;
            }
        }
    }
}