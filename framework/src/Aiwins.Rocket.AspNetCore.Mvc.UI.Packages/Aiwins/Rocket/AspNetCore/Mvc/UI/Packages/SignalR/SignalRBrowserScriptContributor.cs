using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Core;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.SignalR {
    [DependsOn (typeof (CoreScriptContributor))]
    public class SignalRBrowserScriptContributor : BundleContributor {
        public override void ConfigureBundle (BundleConfigurationContext context) {
            context.Files.AddIfNotContains ("/libs/signalr/browser/signalr.js");
        }
    }
}