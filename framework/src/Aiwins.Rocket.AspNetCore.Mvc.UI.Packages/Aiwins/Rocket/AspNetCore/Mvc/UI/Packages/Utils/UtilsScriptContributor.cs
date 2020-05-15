using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Utils {
    public class UtilsScriptContributor : BundleContributor {
        public override void ConfigureBundle (BundleConfigurationContext context) {
            context.Files.AddIfNotContains ("/libs/aiwins/utils/rocket-utils.umd.min.js");
        }
    }
}