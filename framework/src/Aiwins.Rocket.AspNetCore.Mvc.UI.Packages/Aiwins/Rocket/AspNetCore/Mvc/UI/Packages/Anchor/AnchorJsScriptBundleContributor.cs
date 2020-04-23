using System;
using System.Collections.Generic;
using System.Text;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.JQuery;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Anchor
{
    [DependsOn(typeof(JQueryScriptContributor))]
    public class AnchorJsScriptBundleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/anchor-js/anchor.js");
        }
    }
}
