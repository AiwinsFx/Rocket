using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Utils;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Core {
    [DependsOn (typeof (UtilsScriptContributor))]
    public class CoreScriptContributor : BundleContributor {
        public override void ConfigureBundle (BundleConfigurationContext context) {
            context.Files.AddIfNotContains ("/libs/rocket/core/rocket.js");
        }
    }
}